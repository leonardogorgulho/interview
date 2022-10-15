using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class DocumentWithFile : Document
    {
        public byte[] Content { get; set; }
    }
}
