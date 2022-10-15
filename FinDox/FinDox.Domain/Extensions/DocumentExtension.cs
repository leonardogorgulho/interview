using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;

namespace FinDox.Domain.Extensions
{
    public static class DocumentExtension
    {
        public static Document ToEntity(this DocumentWithFile request, int? id = null, int? fileId = null)
        {
            return new Document
            {
                DocumentId = id ?? 0,
                Name = request.Name,
                Description = request.Description,
                PostedDate = request.PostedDate,
                Category = request.Category,
                Size = request.Size,
                ContentType = request.ContentType,
                FileId = fileId ?? 0
            };
        }

        public static DocumentWithFile ToDocumentEntry(this Document request)
        {
            return new DocumentWithFile
            {
                DocumentId = request.DocumentId,
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
