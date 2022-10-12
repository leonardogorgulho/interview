using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetDocumentQuery : IRequest<DocumentResponse>
    {
        public GetDocumentQuery(int id, bool withFile)
        {
            Id = id;
            WithFile = withFile;
        }

        public int Id { get; }

        public bool WithFile { get; }
    }
}
