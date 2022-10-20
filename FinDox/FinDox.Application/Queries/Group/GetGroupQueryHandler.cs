using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, Group?>
    {
        IGroupRepository _groupRepository;

        public GetGroupQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group?> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.Get(request.Id);
            return group;
        }
    }
}
