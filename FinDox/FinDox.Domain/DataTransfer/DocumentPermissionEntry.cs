namespace FinDox.Domain.DataTransfer
{
    public class DocumentPermissionEntry
    {
        public int DocumentId { get; set; }

        public int[] GroupIds { get; set; }

        public int[] UserIds { get; set; }
    }
}