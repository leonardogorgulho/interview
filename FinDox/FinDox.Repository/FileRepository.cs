using Dapper;
using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using NpgsqlTypes;
using System.Data;

namespace FinDox.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly AppConnectionFactory _appConnectionFactory;

        public FileRepository(AppConnectionFactory appConnectionFactory)
        {
            _appConnectionFactory = appConnectionFactory;
        }

        public async Task<int> AddFile(byte[] fileContent)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var id = await connection.ExecuteScalarAsync<int>(
                "core.add_file",
            new
            {
                p_file = _appConnectionFactory.CreateParameter<byte[]>(NpgsqlDbType.Bytea, fileContent)
            },
            commandType: CommandType.StoredProcedure);

            return id;
        }

        public async Task<DocumentFile> GetFile(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<DocumentFile>(
                "core.get_file",
                new { p_file_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
