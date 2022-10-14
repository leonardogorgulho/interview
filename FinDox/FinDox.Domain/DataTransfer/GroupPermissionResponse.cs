using FinDox.Domain.Entities;

namespace FinDox.Domain.DataTransfer
{
    public class GroupPermissionResponse : Group
    {
        public GroupPermissionResponse(int groupId, string groupName)
        {
            GroupId = groupId;
            Name = groupName;
        }

        public List<Document> Documents { get; set; } = new();
    }
}
