using FinDox.Domain.DataTransfer;
using FluentValidation;

namespace FinDox.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Login)
                .NotEmpty()
                .WithMessage("Login field is required");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("Password field is required")
                .Must(p => p.Length == 32)
                .WithMessage("Password not valid");
        }
    }
}
