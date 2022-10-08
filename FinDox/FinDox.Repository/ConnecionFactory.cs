using FinDox.Domain.Types;
using Npgsql;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public sealed class AppConnectionFactory
    {
        public AppConnectionFactory()
        {
            NpgsqlConnection.GlobalTypeMapper.MapComposite<UserEntry>("core.user_entry");
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=findox;");
        }

        public ICustomQueryParameter CreateParameter<T>(string npgTypeName, T entry)
        {
            return new CompositeTypeParameter<T>(npgTypeName, entry);
        }
    }
}
