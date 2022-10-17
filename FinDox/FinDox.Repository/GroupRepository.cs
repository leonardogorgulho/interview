using Dapper;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppConnectionFactory _appConnectionFactory;
        private readonly ILogger<IGroupRepository> _logger;

        public GroupRepository(AppConnectionFactory appConnectionFactory, ILogger<IGroupRepository> logger)
        {
            _appConnectionFactory = appConnectionFactory;
            _logger = logger;
        }

        public async Task<Group> Add(Group entity)
        {
            try
            {
                using var connection = _appConnectionFactory.GetConnection();
                var id = await connection.ExecuteScalarAsync<int>("core.add_group",
                new
                {
                    p_name = entity.Name
                },
                commandType: CommandType.StoredProcedure);

                entity.GroupId = id;
                return entity;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _logger.LogError(ex.Message);
                throw new ExistingGroupException(entity.Name);
            }
        }

        public async Task<bool> AddUser(UserGroup userGroup)
        {
            try
            {
                using var connection = _appConnectionFactory.GetConnection();
                var result = await connection.ExecuteScalarAsync<bool>(
                    "core.add_user_group",
                    new
                    {
                        p_user_id = userGroup.UserId,
                        p_group_id = userGroup.GroupId
                    },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.ForeignKeyViolation)
            {
                _logger.LogError(ex.Message);
                throw new InvalidUserGroupException(userGroup.UserId, userGroup.GroupId);
            }
        }

        public async Task<bool> RemoveUser(UserGroup userGroup)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.ExecuteScalarAsync<bool>(
                "core.delete_user_group",
                new
                {
                    p_user_id = userGroup.UserId,
                    p_group_id = userGroup.GroupId
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<Group?> Get(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<Group>(
                "core.get_group",
                new { p_group_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> Remove(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();
            var affected = await connection.ExecuteScalarAsync<int>(
                "core.delete_group",
                new { p_group_id = id },
                commandType: CommandType.StoredProcedure);

            return affected > 0;
        }

        public async Task<Group> Update(Group entity)
        {
            try
            {
                using var connection = _appConnectionFactory.GetConnection();
                var affected = await connection.ExecuteScalarAsync<int>(
                    "core.update_group",
                    new
                    {
                        p_group_id = entity.GroupId,
                        p_name = entity.Name
                    },
                    commandType: CommandType.StoredProcedure);

                return affected > 0 ? entity : null;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _logger.LogError(ex.Message);
                throw new ExistingGroupException(entity.Name);
            }
        }

        public async Task<GroupWithUsers> GetGroupWithUsers(int groupId)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var group = await connection.QueryFirstOrDefaultAsync<Group>(
                "core.get_group",
                new { p_group_id = groupId },
                commandType: CommandType.StoredProcedure);

            if (group != null)
            {
                var users = await connection.QueryAsync<UserResponse>(
                    "core.get_users_from_group",
                    new { p_group_id = groupId },
                    commandType: CommandType.StoredProcedure);


                return new GroupWithUsers()
                {
                    GroupId = group.GroupId,
                    Name = group.Name,
                    Users = users?.ToList(),
                };
            }

            return null;
        }

        public async Task<List<Group>> GetGroups(string name, int skip, int take)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryAsync<Group>(
                "core.get_groups",
                new
                {
                    p_name = name,
                    p_offset = skip,
                    p_limit = take
                },
                commandType: CommandType.StoredProcedure);

            return result.ToList();
        }
    }
}
