using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveNewUserCommand : IRequest<CommandResult<UserResponse>>
    {
        public SaveNewUserCommand(NewUserRequest userEntry)
        {
            UserEntry = userEntry;
        }

        public NewUserRequest UserEntry { get; set; }
    }
}
