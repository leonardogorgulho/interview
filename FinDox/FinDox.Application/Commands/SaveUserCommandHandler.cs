using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, UserResponse>
    {
        IUserRepository _userRepository;

        public SaveUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.IsNewUser ?
                await _userRepository.Add(request.UserEntry.ToEntity()) :
                await _userRepository.Update(request.UserEntry.ToEntity(request.Id));

            return user.ToUserResponse();
        }
    }
}
