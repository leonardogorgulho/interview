using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveGroupCommandHandler : IRequestHandler<SaveGroupCommand, CommandResult<Group>>
    {
        private readonly IGroupRepository _groupRepository;

        public SaveGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<CommandResult<Group>> Handle(SaveGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var group = request.IsNewGroup ?
                    await _groupRepository.Add(request.GroupRequest.ToEntity()) :
                    await _groupRepository.Update(request.GroupRequest.ToEntity(request.Id));

                return CommandResult<Group>.Success(group);
            }
            catch (ExistingGroupException ex)
            {
                return CommandResult<Group>.WithFailure(ex.Message);
            }
        }
    }
}
