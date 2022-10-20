using FinDox.Domain.Entities;
using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveUserFromGroupCommand : IRequest<bool>
    {
        public RemoveUserFromGroupCommand(UserGroup userGroup)
        {
            UserGroup = userGroup;
        }
        public UserGroup UserGroup { get; set; }
    }
}
