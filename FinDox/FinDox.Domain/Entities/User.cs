namespace FinDox.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
