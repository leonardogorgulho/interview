using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveDocumentPermissionHandler :
        IRequestHandler<GrantDocumentPermission, bool>,
        IRequestHandler<RemoveDocumentPermission, bool>
    {
        private readonly IDocumentRepository _documentRepository;

        public SaveDocumentPermissionHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<bool> Handle(GrantDocumentPermission request, CancellationToken cancellationToken)
        {
            return await _documentRepository.GrantAccess(request.Permission);
        }

        public async Task<bool> Handle(RemoveDocumentPermission request, CancellationToken cancellationToken)
        {
            return await _documentRepository.RemoveAccess(request.Permission);
        }
    }
}
