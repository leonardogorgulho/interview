using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetDocumentPermissionQuery : IRequest<DocumentPermissionResponse>
    {
        public GetDocumentPermissionQuery(int documentId)
        {
            DocumentId = documentId;
        }

        public int DocumentId { get; set; }
    }
}
