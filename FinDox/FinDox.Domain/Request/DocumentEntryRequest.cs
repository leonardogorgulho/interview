namespace FinDox.Domain.Request
{
    public class DocumentEntryRequest
    {
        public DateTime PostedDate { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string ContentType { get; set; }

        public long? Size { get; set; }

        public byte[] FileContent { get; set; }
    }
}
