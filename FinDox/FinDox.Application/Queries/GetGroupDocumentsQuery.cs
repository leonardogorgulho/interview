using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetGroupDocumentsQuery : IRequest<GroupPermissionResponse>
    {
        public GetGroupDocumentsQuery(int groupId)
        {
            GroupId = groupId;
        }

        public int GroupId { get; }
    }
}
