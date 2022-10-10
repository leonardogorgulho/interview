using FinDox.Domain.Entities;

namespace FinDox.Domain.Response
{
    public class DocumentResponse : Document
    {
        public byte[] Content { get; set; }
    }
}
