﻿
using Dapper;
using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Response;
using System.Data;

namespace FinDox.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private AppConnectionFactory _appConnectionFactory;

        public GroupRepository(AppConnectionFactory appConnectionFactory)
        {
            _appConnectionFactory = appConnectionFactory;
        }

        public async Task<Group?> Add(Group entity)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var id = await connection.ExecuteScalarAsync<int>("core.add_group",
            new
            {
                p_name = entity.Name
            },
            commandType: CommandType.StoredProcedure);

            entity.Id = id;

            return entity;
        }

        public async Task<bool> AddUser(UserGroup userGroup)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.ExecuteScalarAsync<bool>(
                "core.add_user_group",
                new
                {
                    p_userid = userGroup.UserId,
                    p_groupid = userGroup.GroupId
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> RemoveUser(UserGroup userGroup)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.ExecuteScalarAsync<bool>(
                "core.remove_user_group",
                new
                {
                    p_userid = userGroup.UserId,
                    p_groupid = userGroup.GroupId
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<Group?> Get(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<Group>(
                "core.get_group",
                new { p_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> Remove(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.delete_group",
                    new { p_id = id },
                    commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Group?> Update(Group entity)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.update_group",
                    new
                    {
                        p_id = entity.Id,
                        p_name = entity.Name
                    },
                    commandType: CommandType.StoredProcedure);

                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GroupWithUsers> GetGroupWithUsers(int groupId)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var group = await connection.QueryFirstOrDefaultAsync<Group>(
                "core.get_group",
                new { p_id = groupId },
                commandType: CommandType.StoredProcedure);

            if (group != null)
            {
                var users = await connection.QueryAsync<UserResponse>(
                    "core.get_users_from_group",
                    new { p_groupid = groupId },
                    commandType: CommandType.StoredProcedure);


                return new GroupWithUsers()
                {
                    Id = group.Id,
                    Name = group.Name,
                    Users = users?.ToList(),
                };
            }

            return null;
        }
    }
}
