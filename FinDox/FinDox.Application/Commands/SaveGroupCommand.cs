using FinDox.Domain.Entities;
using FinDox.Domain.Request;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveGroupCommand : IRequest<Group>
    {
        public SaveGroupCommand(GroupRequest groupRequest)
        {
            GroupRequest = groupRequest;
        }

        public SaveGroupCommand(GroupRequest groupRequest, int? id)
        {
            GroupRequest = groupRequest;
            Id = id;
        }

        public int? Id { get; set; }

        public GroupRequest GroupRequest { get; set; }

        public bool IsNewGroup => !Id.HasValue;
    }
}
