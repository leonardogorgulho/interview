using FinDox.Domain.Interfaces;
using MediatR;

namespace FinDox.Application.Commands
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, bool>
    {
        IUserRepository _userRepository;

        public RemoveUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.Remove(request.Id);
        }
    }
}
