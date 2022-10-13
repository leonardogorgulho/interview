using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class DocumentPermissions : Document
    {
        public Group[] Groups { get; set; }

        public UserResponse[] Users { get; set; }
    }
}
