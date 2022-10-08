using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    internal class CompositeTypeParameter<T> : ICustomQueryParameter
    {
        private T _entry;
        private string _npgTypeName;

        public CompositeTypeParameter(string npgTypeName, T entry)
        {
            _npgTypeName = npgTypeName;
            _entry = entry;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter
            {
                ParameterName = name,
                Value = _entry,
                DataTypeName = _npgTypeName
            };
            command.Parameters.Add(parameter);
        }
    }
}
