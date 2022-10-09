using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Response;

namespace FinDox.Domain.Extensions
{
    public static class UserExtension
    {
        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name
            };
        }

        public static User ToEntity(this UserEntryRequest request, int? id = null)
        {
            return new User
            {
                Id = id ?? 0,
                Name = request.Name,
                Login = request.Login,
                Password = request.Password
            };
        }


    }
}
