using FinDox.Domain.DataTransfer;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetDocumentPermissionQueryHandler : IRequestHandler<GetDocumentPermissionQuery, DocumentPermissionResponse>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentPermissionQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentPermissionResponse> Handle(GetDocumentPermissionQuery request, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetDocumentPermission(request.DocumentId);
        }
    }
}
