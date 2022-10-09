using FinDox.Domain.Interfaces;
using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUsersFromGroupQueryHandler : IRequestHandler<GetUsersFromGroupQuery, GroupWithUsers>
    {
        readonly IGroupRepository _groupRepository;

        public GetUsersFromGroupQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<GroupWithUsers> Handle(GetUsersFromGroupQuery request, CancellationToken cancellationToken)
        {
            return await _groupRepository.GetGroupWithUsers(request.GroupId);
        }
    }
}
