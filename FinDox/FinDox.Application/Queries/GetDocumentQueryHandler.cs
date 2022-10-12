using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, DocumentResponse?>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentResponse?> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            return request.WithFile ?
                await _documentRepository.GetDocumentWithFile(request.Id) :
                (await _documentRepository.Get(request.Id))?.ToDocumentEntry();
        }
    }
}
