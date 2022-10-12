using FinDox.Domain.Entities;
using FinDox.Domain.Request;
using FinDox.Domain.Response;

namespace FinDox.Domain.Extensions
{
    public static class DocumentExtension
    {
        public static Document ToEntity(this DocumentEntryRequest request, int? id = null, int? fileId = null)
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

        public static DocumentResponse ToDocumentEntry(this Document request)
        {
            return new DocumentResponse
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
