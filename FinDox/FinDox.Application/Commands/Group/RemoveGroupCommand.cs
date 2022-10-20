using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveGroupCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public RemoveGroupCommand(int id)
        {
            Id = id;
        }
    }
}
