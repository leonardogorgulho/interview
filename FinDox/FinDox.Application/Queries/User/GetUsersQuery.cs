using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {
        public GetUsersQuery(string name, string login, int skip, int take)
        {
            Name = name;
            Login = login;
            Skip = skip;
            Take = take;
        }

        public string Name { get; }
        public string Login { get; }
        public int Skip { get; }
        public int Take { get; }
    }
}
