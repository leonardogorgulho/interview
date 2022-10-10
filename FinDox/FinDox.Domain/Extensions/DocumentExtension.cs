using FinDox.Domain.Entities;
using FinDox.Domain.Request;

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
                FileId = fileId ?? 0
            };
        }
    }
}
