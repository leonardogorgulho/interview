using FinDox.Domain.Interfaces;
using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        IUserRepository _userRepository;

        public LoginQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Login(request.LoginRequest);

            if (user == null)
            {
                return null;
            }

            //get token

            return new LoginResponse
            {
                User = user,
                Token = ""
            };
        }
    }
}
