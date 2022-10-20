using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, List<Group>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<Group>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _groupRepository.GetGroups(string.Concat("%", request.Name, "%"), request.Skip, request.Take);
        }
    }
}
