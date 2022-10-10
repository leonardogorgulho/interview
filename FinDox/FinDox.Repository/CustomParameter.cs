using Npgsql;
using NpgsqlTypes;
using System.Data;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    internal class CustomParameter<T> : ICustomQueryParameter
    {
        private readonly T _entry;
        private readonly string? _npgTypeName = null;
        private readonly NpgsqlDbType? _dbType = null;

        public CustomParameter(string? npgTypeName, T entry)
        {
            _npgTypeName = npgTypeName;
            _entry = entry;
        }

        public CustomParameter(NpgsqlDbType dbType, T entry)
        {
            _dbType = dbType;
            _entry = entry;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter()
            {
                ParameterName = name,
                Value = _entry,
            };
            if (!string.IsNullOrEmpty(_npgTypeName))
                parameter.DataTypeName = _npgTypeName;
            if (_dbType.HasValue)
                parameter.NpgsqlDbType = _dbType.Value;
            command.Parameters.Add(parameter);
        }
    }
}
