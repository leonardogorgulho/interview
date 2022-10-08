using FinDox.Domain.Entities;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetGroupQuery : IRequest<Group>
    {
        public GetGroupQuery(int id)
        {
            Id = id;
        }

        public int Id { get; internal set; }
    }
}
