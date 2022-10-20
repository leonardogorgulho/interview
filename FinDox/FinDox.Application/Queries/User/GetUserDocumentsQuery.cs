using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUserDocumentsQuery : IRequest<UserPermissionResponse>
    {
        public GetUserDocumentsQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
