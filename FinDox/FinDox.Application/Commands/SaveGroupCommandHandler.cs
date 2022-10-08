using FinDox.Domain.Entities;
using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveGroupCommandHandler : IRequestHandler<SaveGroupCommand, Group?>
    {
        IGroupRepository _groupRepository;

        public SaveGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group?> Handle(SaveGroupCommand request, CancellationToken cancellationToken)
        {
            var group = request.IsNewGroup ?
                await _groupRepository.Add(request.GroupRequest.ToEntity()) :
                await _groupRepository.Update(request.GroupRequest.ToEntity(request.Id));

            return group;
        }
    }
}
