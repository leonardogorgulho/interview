using FinDox.Domain.Entities;
using FinDox.Domain.Request;

namespace FinDox.Domain.Extensions
{
    public static class GroupExtension
    {
        public static Group ToEntity(this GroupRequest request, int? id = null)
        {
            return new Group
            {
                Id = id ?? 0,
                Name = request.Name
            };
        }
    }
}
