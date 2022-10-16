﻿using Dapper;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Types;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppConnectionFactory _appConnectionFactory;

        public UserRepository(AppConnectionFactory appConnectionFactory)
        {
            _appConnectionFactory = appConnectionFactory;
        }

        public async Task<User?> Add(User entity)
        {
            try
            {
                using var connection = _appConnectionFactory.GetConnection();
                var id = await connection.ExecuteScalarAsync<int>("core.add_user",
                new
                {
                    p_user = _appConnectionFactory.CreateParameter<UserEntry>("core.user_entry", UserEntry.MapFrom(entity))
                },
                commandType: CommandType.StoredProcedure);

                entity.UserId = id;

                return entity;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                //log
                throw new ExistingLoginException(entity.Login);
            }
        }

        public async Task<User?> Get(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<User>(
                "core.get_user",
                new { p_user_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<List<UserResponse>> GetUsers(string name, string login, int skip, int take)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryAsync<UserResponse>(
                "core.get_users",
                new
                {
                    p_name = name,
                    p_login = login,
                    p_offset = skip,
                    p_limit = take
                },
                commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<UserResponse> Login(LoginRequest request)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<UserResponse>(
                "core.login",
                new { p_login = request.Login, p_password = request.Password },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> Remove(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.delete_user",
                    new { p_user_id = id },
                    commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User> Update(User entity)
        {
            try
            {
                using var connection = _appConnectionFactory.GetConnection();
                var affected = await connection.ExecuteScalarAsync<int>(
                    "core.update_user",
                    new
                    {
                        p_user_id = entity.UserId,
                        p_user = _appConnectionFactory.CreateParameter<UserEntry>("core.user_entry", UserEntry.MapFrom(entity))
                    },
                    commandType: CommandType.StoredProcedure);

                return affected > 0 ? entity : null;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                //log
                throw new ExistingLoginException(entity.Login);
            }
        }
    }
}