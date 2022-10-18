using FinDox.Domain.DataTransfer;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveDocumentPermissionHandler :
        IRequestHandler<GrantDocumentPermission, CommandResult<bool>>,
        IRequestHandler<RemoveDocumentPermission, CommandResult<bool>>
    {
        private readonly IDocumentRepository _documentRepository;

        public SaveDocumentPermissionHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<CommandResult<bool>> Handle(GrantDocumentPermission request, CancellationToken cancellationToken)
        {
            try
            {
                var grantedObject = await _documentRepository.GrantAccess(request.Permission);
                return CommandResult<bool>.Success(grantedObject);
            }
            catch (InvalidDocumentPermissionEntryException ex)
            {
                return CommandResult<bool>.WithFailure(ex.Message);
            }
        }

        public async Task<CommandResult<bool>> Handle(RemoveDocumentPermission request, CancellationToken cancellationToken)
        {
            return CommandResult<bool>.Success(await _documentRepository.RemoveAccess(request.Permission));
        }
    }
}
