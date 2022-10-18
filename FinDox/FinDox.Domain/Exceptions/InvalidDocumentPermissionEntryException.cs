using FinDox.Domain.DataTransfer;

namespace FinDox.Domain.Exceptions
{
    public class InvalidDocumentPermissionEntryException : Exception
    {
        public InvalidDocumentPermissionEntryException(DocumentPermissionEntry entry)
            : base($"One or more provided ids are not valid (UserIds: {string.Join(", ", entry.UserIds)}, GroupIds: {string.Join(", ", entry.GroupIds)})")
        {

        }
    }
}
