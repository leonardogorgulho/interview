﻿using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using MediatR;

namespace FinDox.Application.Commands
{
    public class AddDocumentCommand : IRequest<Document>
    {
        public AddDocumentCommand(Domain.DataTransfer.DocumentFile document, byte[] fileContent)
        {
            Document = document;
            FileContent = fileContent;
        }

        public Domain.DataTransfer.DocumentFile Document { get; set; }

        public byte[] FileContent { get; set; }
    }
}
