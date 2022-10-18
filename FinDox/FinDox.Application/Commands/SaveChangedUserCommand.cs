using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveChangedUserCommand : IRequest<CommandResult<UserResponse>>
    {
        public SaveChangedUserCommand(ChangeUserRequest userEntry, int id)
        {
            Id = id;
            UserEntry = userEntry;
        }

        public int Id { get; }

        public ChangeUserRequest UserEntry { get; set; }
    }
}
