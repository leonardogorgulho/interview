namespace FinDox.Domain.Exceptions
{
    public class InvalidUserGroupException : Exception
    {
        public InvalidUserGroupException(int userId, int groupId) : base($"One or more value is not correct (user id: {userId}, group id: {groupId})")
        {

        }
    }
}
