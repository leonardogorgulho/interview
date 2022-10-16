using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand, bool>
    {
        private readonly IGroupRepository _groupRepository;

        public AddUserToGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            return await _groupRepository.AddUser(request.UserGroup);
        }
    }
}
