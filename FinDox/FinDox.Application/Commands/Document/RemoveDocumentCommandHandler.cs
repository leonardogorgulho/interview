using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveDocumentCommandHandler : IRequestHandler<RemoveDocumentCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;

        public RemoveDocumentCommandHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<bool> Handle(RemoveDocumentCommand request, CancellationToken cancellationToken)
        {
            return await _documentRepository.Remove(request.DocumentId);
        }
    }
}
