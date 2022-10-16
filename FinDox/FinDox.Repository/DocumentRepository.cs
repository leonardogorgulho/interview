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

        public async Task<Document> Add(Document entity)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var id = await connection.ExecuteScalarAsync<int>("core.add_document",
            new
            {
                p_document = _appConnectionFactory.CreateParameter<DocumentEntry>("core.document_entry", DocumentEntry.MapFrom(entity))
            },
            commandType: CommandType.StoredProcedure);

            entity.DocumentId = id;

            return entity;
        }

        public async Task<bool> CanUserDownloadDocument(int userId, int documentId)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.ExecuteScalarAsync<bool>(
                "core.can_user_download_document",
                new { p_user_id = userId, p_document_id = documentId },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<Document> Get(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<Document>(
                "core.get_document",
                new { p_document_id = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<DocumentPermissionResponse> GetDocumentPermission(int documentId)
        {
            using var connection = _appConnectionFactory.GetConnection();
            DocumentPermissionResponse document = null;
            var result = await connection.QueryAsync<
                    DocumentPermissionResponse,
                    (int GroupId, string GroupName),
                    (int UserId, string UserName, string Login),
                    DocumentPermissionResponse>(
                "core.get_document_permissions",
                (doc, grp, user) =>
                {
                    if (document == null)
                        document = doc;

                    if (grp.GroupId > 0)
                        document.Groups.Add(new() { GroupId = grp.GroupId, Name = grp.GroupName });

                    if (user.UserId > 0)
                        document.Users.Add(new() { UserId = user.UserId, Name = user.UserName, Login = user.Login });

                    return doc;
                },
                new { p_document_id = documentId },
                splitOn: "group_id,user_id",
                commandType: CommandType.StoredProcedure);

            return document;
        }

        public async Task<GroupPermissionResponse> GetDocumentsByGroup(int groupId)
        {
            using var connection = _appConnectionFactory.GetConnection();
            GroupPermissionResponse groupPermission = null;
            var result = await connection.QueryAsync<
                    (int GroupId, string GroupName),
                    Document,
                    GroupPermissionResponse>(
                "core.get_document_by_group",
                (grp, doc) =>
                {
                    if (groupPermission == null)
                        groupPermission = new(grp.GroupId, grp.GroupName);

                    groupPermission.Documents.Add(doc);

                    return groupPermission;
                },
                new { p_group_id = groupId },
                splitOn: "document_id",
                commandType: CommandType.StoredProcedure);

            return groupPermission;
        }

        public async Task<UserPermissionResponse> GetDocumentsByUser(int userId)
        {
            using var connection = _appConnectionFactory.GetConnection();
            UserPermissionResponse userPermission = null;
            var result = await connection.QueryAsync<
                    (int UserId, string UserName, string Login),
                    Document,
                    UserPermissionResponse>(
                "core.get_document_by_user",
                (user, doc) =>
                {
                    if (userPermission == null)
                        userPermission = new(user.UserId, user.UserName, user.Login);

                    userPermission.Documents.Add(doc);

                    return userPermission;
                },
                new { p_user_id = userId },
                splitOn: "document_id",
                commandType: CommandType.StoredProcedure);

            return userPermission;
        }

        public async Task<DocumentWithFile> GetDocumentWithFile(int id)
        {
            using var connection = _appConnectionFactory.GetConnection();

            var result = await connection.QueryFirstOrDefaultAsync<Domain.DataTransfer.DocumentWithFile>(
                "core.get_document_with_file",
                new { p_document_id = id },
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
                        p_document_id = documentPermission.DocumentId,
                        p_group_ids = documentPermission.GroupIds,
                        p_user_ids = documentPermission.UserIds
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
                    new { p_document_id = id },
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
                        p_document_id = documentPermission.DocumentId,
                        p_group_ids = documentPermission.GroupIds,
                        p_user_ids = documentPermission.UserIds
                    },
                    commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Document> Update(Document entity)
        {
            using var connection = _appConnectionFactory.GetConnection();
            var affected = await connection.ExecuteScalarAsync<int>(
                "core.update_document",
                new
                {
                    p_document_id = entity.DocumentId,
                    p_document = _appConnectionFactory.CreateParameter<DocumentEntry>("core.document_entry", DocumentEntry.MapFrom(entity))
                },
                commandType: CommandType.StoredProcedure);

            return affected > 0 ? entity : null;
        }
    }
}
