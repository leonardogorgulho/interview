using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetDocumentQuery : IRequest<DocumentFile>
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
