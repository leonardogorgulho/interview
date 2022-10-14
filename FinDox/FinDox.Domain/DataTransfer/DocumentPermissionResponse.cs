using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class DocumentPermissionResponse : Document
    {
        public List<Group> Groups { get; set; } = new();

        public List<UserResponse> Users { get; set; } = new();
    }
}
