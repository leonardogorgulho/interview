using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Queries
{
    public class CanUserDownloadDocumentQueryHandler : IRequestHandler<CanUserDownloadDocumentQuery, bool>
    {
        private readonly IDocumentRepository _documentRepository;

        public CanUserDownloadDocumentQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<bool> Handle(CanUserDownloadDocumentQuery request, CancellationToken cancellationToken)
        {
            return await _documentRepository.CanUserDownloadDocument(request.UserId, request.DocumentId);
        }
    }
}
