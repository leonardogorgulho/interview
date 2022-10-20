using FinDox.Domain.DataTransfer;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetGroupDocumentsQueryHandler : IRequestHandler<GetGroupDocumentsQuery, GroupPermissionResponse>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetGroupDocumentsQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<GroupPermissionResponse> Handle(GetGroupDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetDocumentsByGroup(request.GroupId);
        }
    }
}
