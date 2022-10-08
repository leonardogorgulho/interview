using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveGroupCommandHandler : IRequestHandler<RemoveGroupCommand, bool>
    {
        IGroupRepository _groupRepository;

        public RemoveGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
        {
            return await _groupRepository.Remove(request.Id);
        }
    }
}
