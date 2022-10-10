using FinDox.Domain.Entities;
using FinDox.Domain.Request;
using MediatR;

namespace FinDox.Application.Commands
{
    public class AddDocumentCommand : IRequest<Document>
    {
        public AddDocumentCommand(DocumentEntryRequest document, byte[] fileContent)
        {
            Document = document;
            FileContent = fileContent;
        }

        public DocumentEntryRequest Document { get; set; }

        public byte[] FileContent { get; set; }
    }
}
