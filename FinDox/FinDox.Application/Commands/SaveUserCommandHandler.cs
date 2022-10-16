using FinDox.Domain.DataTransfer;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, CommandResult<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public SaveUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CommandResult<UserResponse>> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = request.IsNewUser ?
                    await _userRepository.Add(request.UserEntry.ToEntity()) :
                    await _userRepository.Update(request.UserEntry.ToEntity(request.Id));

                return CommandResult<UserResponse>.Success(user?.ToUserResponse());
            }
            catch (ExistingLoginException ex)
            {
                return CommandResult<UserResponse>.WithFailure(ex.Message);
            }
        }
    }
}
