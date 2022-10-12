using Dapper;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Types;
using System.Data;
using static Dapper.SqlMapper;

namespace FinDox.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppConnectionFactory _appConnectionFactory;

        public DocumentRepository(AppConnectionFactory appConnectionFactory)
        {
            _appConnectionFactory = appConnectionFactory;
        }

        public async Task<Document?> Add(Document entity)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var id = await connection.ExecuteScalarAsync<int>("core.add_document",
            new
            {
                p_document = _appConnectionFactory.CreateParameter<DocumentEntry>("core.document_entry", DocumentEntry.MapFrom(entity))
            },
            commandType: CommandType.StoredProcedure);

            entity.Id = id;

            return entity;
        }

        public async Task<Document?> Get(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<Document>(
                "core.get_document",
                new { p_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<Domain.DataTransfer.DocumentFile?> GetDocumentWithFile(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<Domain.DataTransfer.DocumentFile>(
                "core.get_document_with_file",
                new { p_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> GrantAccess(DocumentPermissionEntry documentPermission)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.add_document_permission",
                    new
                    {
                        p_documentid = documentPermission.DocumentId,
                        p_groupids = documentPermission.GroupIds,
                        p_userids = documentPermission.UserIds
                    },
                    commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.delete_document",
                    new { p_id = id },
                    commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveAccess(DocumentPermissionEntry documentPermission)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.delete_document_permission",
                    new
                    {
                        p_documentid = documentPermission.DocumentId,
                        p_groupids = documentPermission.GroupIds,
                        p_userids = documentPermission.UserIds
                    },
                    commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Document?> Update(Document entity)
        {
            using var connection = _appConnectionFactory.GetConnection();

            try
            {
                await connection.ExecuteAsync(
                    "core.update_document",
                    new
                    {
                        p_id = entity.Id,
                        p_document = _appConnectionFactory.CreateParameter<DocumentEntry>("core.document_entry", DocumentEntry.MapFrom(entity))
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
