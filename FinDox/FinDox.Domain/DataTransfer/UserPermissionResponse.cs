using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class UserPermissionResponse : UserResponse
    {
        public UserPermissionResponse(int userId, string userName, string login)
        {
            UserId = userId;
            Name = userName;
            Login = login;
        }

        public List<Document> Documents { get; set; } = new();
    }
}
