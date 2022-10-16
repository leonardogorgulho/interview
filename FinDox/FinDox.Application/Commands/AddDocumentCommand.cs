using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using MediatR;

namespace FinDox.Application.Commands
{
    public class AddDocumentCommand : IRequest<CommandResult<Document>>
    {
        public AddDocumentCommand(DocumentWithFile document, byte[] fileContent)
        {
            Document = document;
            FileContent = fileContent;
        }

        public DocumentWithFile Document { get; set; }

        public byte[] FileContent { get; set; }
    }
}
