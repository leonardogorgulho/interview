namespace FinDox.Domain.Entities
{
    public class DocumentPermission
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public int? GroupId { get; set; }

        public int? UserId { get; set; }
    }
}
