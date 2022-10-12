namespace FinDox.Domain.DataTransfer
{
    public class LoginResponse
    {
        public UserResponse User { get; set; }

        public string Token { get; set; }
    }
}
