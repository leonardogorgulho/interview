namespace FinDox.Domain.Exceptions
{
    public class ExistingLoginException : Exception
    {
        public ExistingLoginException(string login) : base($"Login '{login}' already in use by another user")
        {

        }
    }
}
