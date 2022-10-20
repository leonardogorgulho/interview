using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUserQuery : IRequest<UserResponse>
    {
        public GetUserQuery(int id)
        {
            Id = id;
        }

        public int Id { get; internal set; }
    }
}
