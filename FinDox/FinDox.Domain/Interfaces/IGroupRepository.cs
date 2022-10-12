
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;

namespace FinDox.Domain.Interfaces
{
    public interface IGroupRepository : ICRUDRepositoy<Group>
    {
        Task<bool> AddUser(UserGroup userGroup);

        Task<bool> RemoveUser(UserGroup userGroup);

        Task<GroupWithUsers> GetGroupWithUsers(int groupId);
    }
}
