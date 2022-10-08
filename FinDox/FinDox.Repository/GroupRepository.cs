using FinDox.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace FinDox.Repository
{
    public class GroupRepository : IGroupRepository
    {
        public Task<Group?> Add(Group entity)
        {
            throw new NotImplementedException();
        }

        public Task<Group?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group?> Update(Group entity)
        {
            throw new NotImplementedException();
        }
    }
}
