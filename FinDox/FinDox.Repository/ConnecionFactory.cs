using FinDox.Domain.Types;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public sealed class AppConnectionFactory
    {
        IConfiguration _configuration;

        public AppConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            NpgsqlConnection.GlobalTypeMapper.MapComposite<UserEntry>("core.user_entry");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<DocumentEntry>("core.document_entry");
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("Default"));
        }

        public ICustomQueryParameter CreateParameter<T>(string npgTypeName, T entry)
        {
            return new CustomParameter<T>(npgTypeName, entry);
        }

        public ICustomQueryParameter CreateParameter<T>(NpgsqlDbType dbType, T entry)
        {
            return new CustomParameter<T>(dbType, entry);
        }
    }
}
