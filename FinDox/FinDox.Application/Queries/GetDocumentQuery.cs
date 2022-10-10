using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetDocumentQuery : IRequest<DocumentResponse>
    {
        public GetDocumentQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
