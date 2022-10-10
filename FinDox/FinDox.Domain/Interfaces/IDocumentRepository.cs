using FinDox.Domain.Entities;
using FinDox.Domain.Response;

namespace FinDox.Domain.Interfaces
{
    public interface IDocumentRepository : ICRUDRepositoy<Document>
    {
        Task<DocumentResponse?> GetDocumentWithFile(int id);
    }
}
