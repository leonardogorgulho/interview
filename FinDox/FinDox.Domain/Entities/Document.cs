namespace FinDox.Domain.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }

        public int FileId { get; set; }

        public DateTime PostedDate { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string ContentType { get; set; }

        public long? Size { get; set; }
    }
}
