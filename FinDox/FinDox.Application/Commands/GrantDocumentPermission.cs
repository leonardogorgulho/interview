using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Commands
{
    public class GrantDocumentPermission : IRequest<bool>
    {
        public DocumentPermissionEntry Permission { get; set; }

        public GrantDocumentPermission(DocumentPermissionEntry permission)
        {
            Permission = permission;
        }
    }
}
