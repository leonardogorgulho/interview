using Dapper;
using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Types;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public class UserEntryParameter : ICustomQueryParameter
    {
        private readonly UserEntry _userEntry;

        public UserEntryParameter(string name, string login, string password)
        {
            _userEntry = new UserEntry()
            {
                Name = name,
                Login = login,
                Password = password
            };
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter
            {
                ParameterName = name,
                Value = _userEntry,
                DataTypeName = "core.user_entry"
            };
            command.Parameters.Add(parameter);
        }
    }

    public class UserRepository : IUserRepository
    {
        public async Task<User> Add(User entity)
        {
            using var connection = AppConnectionFactory.GetConnection();

            var id = await connection.ExecuteScalarAsync<int>("core.add_user",
            new
            {
                p_user = AppConnectionFactory.CreateParameter<UserEntry>("core.user_entry", UserEntry.MapFrom(entity))
            },
            commandType: CommandType.StoredProcedure);

            entity.Id = id;

            return entity;
        }

        public async Task<User> Get(int id)
        {
            using var connection = AppConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<User>(
                "core.get_user",
                new { p_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public Task<User> GetAll(IFilter<User> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}