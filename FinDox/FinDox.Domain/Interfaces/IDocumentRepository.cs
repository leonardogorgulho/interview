using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;

namespace FinDox.Domain.Interfaces
{
    public interface IDocumentRepository : ICRUDRepositoy<Document>
    {
        Task<DataTransfer.DocumentFile?> GetDocumentWithFile(int id);

        Task<bool> GrantAccess(DocumentPermissionEntry documentPermission);

        Task<bool> RemoveAccess(DocumentPermissionEntry documentPermission);
    }
}
