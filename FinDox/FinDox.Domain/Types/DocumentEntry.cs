using FinDox.Domain.Entities;

namespace FinDox.Domain.Types
{
    public class DocumentEntry
    {
        public int FileId { get; set; }

        public DateTime? PostedDate { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public static DocumentEntry MapFrom(Document entity)
        {
            return new DocumentEntry
            {
                FileId = entity.FileId,
                PostedDate = entity.PostedDate,
                Name = entity.Name,
                Description = entity.Description,
                Category = entity.Category,
            };
        }
    }
}
