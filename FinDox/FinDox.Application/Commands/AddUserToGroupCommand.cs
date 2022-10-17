using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using MediatR;

namespace FinDox.Application.Commands
{

    public class AddUserToGroupCommand : IRequest<CommandResult<bool>>
    {
        public AddUserToGroupCommand(UserGroup userGroup)
        {
            UserGroup = userGroup;
        }

        public UserGroup UserGroup { get; set; }
    }
}
