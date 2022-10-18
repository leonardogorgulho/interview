namespace FinDox.Domain.Exceptions
{
    public class InvalidUserGroupException : Exception
    {
        public InvalidUserGroupException(int userId, int groupId) : base($"One or more provided Ids are not valid (user id: {userId}, group id: {groupId})")
        {

        }
    }
}
