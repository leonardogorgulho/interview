using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;

namespace FinDox.Domain.Interfaces
{
    public interface IUserRepository : ICRUDRepositoy<User>
    {
        Task<UserResponse> Login(LoginRequest login);

        Task<List<UserResponse>> GetUsers(string name, string login, int skip, int take);
    }
}
