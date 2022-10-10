using FinDox.Domain.Entities;
using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, Document>
    {
        IDocumentRepository _documentRepository;
        IFileRepository _fileRepository;

        public AddDocumentCommandHandler(IDocumentRepository documentRepository, IFileRepository fileRepository)
        {
            _documentRepository = documentRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Document> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
        {
            var fileid = await _fileRepository.AddFile(request.FileContent);
            var document = await _documentRepository.Add(request.Document.ToEntity(fileId: fileid));

            return document;
        }
    }
}
