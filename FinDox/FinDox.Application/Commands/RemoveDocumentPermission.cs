using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveDocumentPermission : IRequest<CommandResult<bool>>
    {
        public DocumentPermissionEntry Permission { get; set; }

        public RemoveDocumentPermission(DocumentPermissionEntry permission)
        {
            Permission = permission;
        }
    }
}
