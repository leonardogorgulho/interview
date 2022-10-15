namespace FinDox.Application.Validators
{
    internal class ValidatorMessages
    {
        public static string RequiredField(string fieldName) => $"{fieldName} field is required";

        public static string MaxLength(string fieldName, int max) => $"{fieldName} field must have a maximum of {max} characters";

        public static string ExactLength(string fieldName, int length) => $"{fieldName} field must have a length of {length} characters";

        public static string RoleError() => "The Role field should have one of the following values: A, M or R";
    }
}
