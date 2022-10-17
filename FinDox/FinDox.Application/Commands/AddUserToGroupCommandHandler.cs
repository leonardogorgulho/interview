using FinDox.Domain.DataTransfer;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand, CommandResult<bool>>
    {
        private readonly IGroupRepository _groupRepository;

        public AddUserToGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<CommandResult<bool>> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _groupRepository.AddUser(request.UserGroup);
                return CommandResult<bool>.Success(result);
            }
            catch (InvalidUserGroupException ex)
            {
                return CommandResult<bool>.WithFailure(ex.Message);
            }
        }
    }
}
