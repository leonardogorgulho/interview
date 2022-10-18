using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class ChangeUserRequest
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Role { get; set; }

        public User ConvertToUser()
        {
            return new User
            {
                Name = Name,
                Login = Login,
                Role = Role
            };
        }
    }
}
