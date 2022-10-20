using FinDox.Domain.DataTransfer;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUsers(
                string.Concat("%", request.Name, "%"),
                string.Concat("%", request.Login, "%"),
                request.Skip,
                request.Take);
        }
    }
}
