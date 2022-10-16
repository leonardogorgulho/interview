namespace FinDox.Domain.Exceptions
{
    public class ExistingGroupException : Exception
    {
        public ExistingGroupException(string groupName) : base($"A group with name '{groupName}' already exists")
        {

        }
    }
}
