using FinDox.Domain.Entities;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetGroupsQuery : IRequest<List<Group>>
    {
        public GetGroupsQuery(string name, int skip, int take)
        {
            Name = name;
            Skip = skip;
            Take = take;
        }

        public string Name { get; }
        public int Skip { get; }
        public int Take { get; }
    }
}
