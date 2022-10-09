
using FinDox.Domain.Entities;
using FinDox.Domain.Response;

namespace FinDox.Domain.Interfaces
{
    public interface IGroupRepository : ICRUDRepositoy<Group>
    {
        Task<bool> AddUser(UserGroup userGroup);

        Task<bool> RemoveUser(UserGroup userGroup);

        Task<GroupWithUsers> GetGroupWithUsers(int groupId);
    }
}
