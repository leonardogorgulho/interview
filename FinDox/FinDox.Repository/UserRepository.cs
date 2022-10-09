using Dapper;
using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Request;
using FinDox.Domain.Response;
using FinDox.Domain.Types;
using System.Data;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public class UserRepository : IUserRepository
    {
        private AppConnectionFactory _appConnectionFactory;

        public UserRepository(AppConnectionFactory appConnectionFactory)
        {
            _appConnectionFactory = appConnectionFactory;
        }

        public async Task<User?> Add(User entity)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var id = await connection.ExecuteScalarAsync<int>("core.add_user",
            new
            {
                p_user = _appConnectionFactory.CreateParameter<UserEntry>("core.user_entry", UserEntry.MapFrom(entity))
            },
            commandType: CommandType.StoredProcedure);

            entity.Id = id;

            return entity;
        }

        public async Task<User?> Get(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<User>(
                "core.get_user",
                new { p_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
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
                    new { p_id = id },
                    commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User?> Update(User entity)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.update_user",
                    new
                    {
                        p_id = entity.Id,
                        p_user = _appConnectionFactory.CreateParameter<UserEntry>("core.user_entry", UserEntry.MapFrom(entity))
                    },
                    commandType: CommandType.StoredProcedure);

                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}