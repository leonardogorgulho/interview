namespace FinDox.Domain.DataTransfer
{
    public class CommandResult<T> where T : class
    {
        public T Data { get; private set; }

        public string[] Errors { get; private set; }

        public bool IsValid => Errors == null;

        public static CommandResult<T> Success(T result)
        {
            return new CommandResult<T>
            {
                Data = result
            };
        }

        public static CommandResult<T> WithFailure(params string[] errors)
        {
            return new CommandResult<T>
            {
                Errors = errors
            };
        }
    }
}
