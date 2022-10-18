using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;

namespace FinDox.Domain.Extensions
{
    public static class UserExtension
    {
        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                Login = user.Login,
                Name = user.Name,
                Role = user.Role
            };
        }

        public static User ToEntity(this NewUserRequest request, int? id = null)
        {
            return new User
            {
                UserId = id ?? 0,
                Name = request.Name,
                Login = request.Login,
                Password = request.Password,
                Role = request.Role
            };
        }

        public static User ToEntity(this ChangeUserRequest request, int? id = null)
        {
            return new User
            {
                UserId = id ?? 0,
                Name = request.Name,
                Login = request.Login,
                Role = request.Role
            };
        }
    }
}
