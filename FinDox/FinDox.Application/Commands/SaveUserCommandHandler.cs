using FinDox.Domain.DataTransfer;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveUserCommandHandler :
        IRequestHandler<SaveNewUserCommand, CommandResult<UserResponse>>,
        IRequestHandler<SaveChangedUserCommand, CommandResult<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public SaveUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CommandResult<UserResponse>> Handle(SaveNewUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.Add(request.UserEntry.ToEntity());

                return CommandResult<UserResponse>.Success(user?.ToUserResponse());
            }
            catch (ExistingLoginException ex)
            {
                return CommandResult<UserResponse>.WithFailure(ex.Message);
            }
        }

        public async Task<CommandResult<UserResponse>> Handle(SaveChangedUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.Update(request.UserEntry.ToEntity(request.Id));

                return CommandResult<UserResponse>.Success(user?.ToUserResponse());
            }
            catch (ExistingLoginException ex)
            {
                return CommandResult<UserResponse>.WithFailure(ex.Message);
            }
        }
    }
}
