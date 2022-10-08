using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveUserCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public RemoveUserCommand(int id)
        {
            Id = id;
        }
    }
}
