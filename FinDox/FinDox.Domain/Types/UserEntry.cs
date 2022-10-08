using FinDox.Domain.Entities;

namespace FinDox.Domain.Types
{
    public class UserEntry
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public static UserEntry MapFrom(User entity)
        {
            return new UserEntry
            {
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password
            };
        }
    }
}
