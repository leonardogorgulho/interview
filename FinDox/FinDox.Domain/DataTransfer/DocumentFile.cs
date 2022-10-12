using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class DocumentFile : Document
    {
        public byte[] Content { get; set; }
    }
}
