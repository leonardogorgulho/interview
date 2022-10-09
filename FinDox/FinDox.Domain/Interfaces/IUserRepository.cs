using FinDox.Domain.Entities;
using FinDox.Domain.Request;
using FinDox.Domain.Response;

namespace FinDox.Domain.Interfaces
{
    public interface IUserRepository : ICRUDRepositoy<User>
    {
        Task<UserResponse> Login(LoginRequest login);
    }
}
