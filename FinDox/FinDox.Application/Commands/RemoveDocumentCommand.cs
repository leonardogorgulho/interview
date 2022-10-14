using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveDocumentCommand : IRequest<bool>
    {
        public RemoveDocumentCommand(int documentId)
        {
            DocumentId = documentId;
        }

        public int DocumentId { get; }
    }
}
