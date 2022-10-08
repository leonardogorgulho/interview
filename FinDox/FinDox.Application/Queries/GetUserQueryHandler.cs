using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
    {
        IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id);
            return user.ToUserResponse();
        }
    }
}
