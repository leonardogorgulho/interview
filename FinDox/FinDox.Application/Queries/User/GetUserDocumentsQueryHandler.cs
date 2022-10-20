using FinDox.Domain.DataTransfer;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUserDocumentsQueryHandler : IRequestHandler<GetUserDocumentsQuery, UserPermissionResponse>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetUserDocumentsQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<UserPermissionResponse> Handle(GetUserDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetDocumentsByUser(request.UserId);
        }
    }
}
