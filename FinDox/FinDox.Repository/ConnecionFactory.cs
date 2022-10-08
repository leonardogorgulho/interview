using FinDox.Domain.Types;
using Npgsql;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public class AppConnectionFactory
    {
        public static NpgsqlConnection GetConnection()
        {
            NpgsqlConnection.GlobalTypeMapper.MapComposite<UserEntry>("core.user_entry");
            return new NpgsqlConnection("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=findox;");
        }

        public static ICustomQueryParameter CreateParameter<T>(string npgTypeName, T entry)
        {
            return new CompositeTypeParameter<T>(npgTypeName, entry);
        }
    }
}
