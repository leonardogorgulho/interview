using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;

namespace FinDox.Domain.Extensions
{
    public static class DocumentExtension
    {
        public static Document ToEntity(this DataTransfer.DocumentFile request, int? id = null, int? fileId = null)
        {
            return new Document
            {
                Id = id ?? 0,
                Name = request.Name,
                Description = request.Description,
                PostedDate = request.PostedDate,
                Category = request.Category,
                Size = request.Size,
                ContentType = request.ContentType,
                FileId = fileId ?? 0
            };
        }

        public static DataTransfer.DocumentFile ToDocumentEntry(this Document request)
        {
            return new DataTransfer.DocumentFile
            {
                Id = request.Id,
                Category = request.Category,
                ContentType = request.ContentType,
                Description = request.Description,
                Name = request.Name,
                PostedDate = request.PostedDate,
                Size = request.Size,
                FileId = request.FileId
            };
        }
    }
}
