using FinDox.Domain.DataTransfer;
using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveUserCommand : IRequest<UserResponse>
    {
        public SaveUserCommand(UserEntryRequest userEntry)
        {
            UserEntry = userEntry;
        }

        public SaveUserCommand(UserEntryRequest userEntry, int? id)
        {
            UserEntry = userEntry;
            Id = id;
        }

        public int? Id { get; set; }

        public UserEntryRequest UserEntry { get; set; }

        public bool IsNewUser => !Id.HasValue;
    }
}
