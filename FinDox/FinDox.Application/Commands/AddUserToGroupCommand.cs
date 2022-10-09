using FinDox.Domain.Entities;
using MediatR;

namespace FinDox.Application.Commands
{

    public class AddUserToGroupCommand : IRequest<bool>
    {
        public AddUserToGroupCommand(UserGroup userGroup)
        {
            UserGroup = userGroup;
        }

        public UserGroup UserGroup { get; set; }
    }
}
