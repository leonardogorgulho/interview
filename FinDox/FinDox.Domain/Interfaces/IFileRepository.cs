using FinDox.Domain.Entities;

namespace FinDox.Domain.Interfaces
{
    public interface IFileRepository
    {
        Task<int> AddFile(byte[] fileContent);

        Task<DocumentFile> GetFile(int id);
    }
}
