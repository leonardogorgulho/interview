using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Queries
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public LoginQuery(LoginRequest loginRequest)
        {
            LoginRequest = loginRequest;
        }
        public LoginRequest LoginRequest { get; set; }
    }
}
