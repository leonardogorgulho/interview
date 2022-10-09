using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand, bool>
    {
        IGroupRepository _groupRepository;

        public RemoveUserFromGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            return await _groupRepository.RemoveUser(request.UserGroup);
        }
    }
}
