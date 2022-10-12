using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class UserEntryRequest
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public User ConvertToUser()
        {
            return new User
            {
                Name = Name,
                Login = Login,
                Password = Password
            };
        }
    }
}
