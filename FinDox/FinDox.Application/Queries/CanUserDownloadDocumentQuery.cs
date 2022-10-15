using MediatR;

namespace FinDox.Application.Queries
{
    public class CanUserDownloadDocumentQuery : IRequest<bool>
    {
        public CanUserDownloadDocumentQuery(int userId, int documentId)
        {
            UserId = userId;
            DocumentId = documentId;
        }

        public int UserId { get; }
        public int DocumentId { get; }
    }
}
