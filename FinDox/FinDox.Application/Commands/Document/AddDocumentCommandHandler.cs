using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, CommandResult<Document>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IFileRepository _fileRepository;

        public AddDocumentCommandHandler(IDocumentRepository documentRepository, IFileRepository fileRepository)
        {
            _documentRepository = documentRepository;
            _fileRepository = fileRepository;
        }

        public async Task<CommandResult<Document>> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
        {
            if (request.FileContent == null)
            {
                return CommandResult<Document>.WithFailure("File content cannot be null");
            }

            var fileid = await _fileRepository.AddFile(request.FileContent);
            var document = await _documentRepository.Add(request.Document.ToEntity(fileId: fileid));

            return CommandResult<Document>.Success(document);
        }
    }
}
