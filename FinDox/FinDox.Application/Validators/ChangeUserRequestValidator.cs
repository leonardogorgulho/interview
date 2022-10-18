using FinDox.Domain.DataTransfer;
using FluentValidation;

namespace FinDox.Application.Validators
{
    public class ChangeUserRequestValidator : AbstractValidator<ChangeUserRequest>
    {
        public ChangeUserRequestValidator()
        {
            RuleFor(entry => entry.Name)
               .NotEmpty()
               .WithMessage(ValidatorMessages.RequiredField(nameof(NewUserRequest.Name)))
               .MaximumLength(150)
               .WithMessage(ValidatorMessages.MaxLength(nameof(NewUserRequest.Name), 150));

            RuleFor(entry => entry.Login)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(NewUserRequest.Login)))
                .MaximumLength(30)
                .WithMessage(ValidatorMessages.MaxLength(nameof(NewUserRequest.Login), 30));

            RuleFor(entry => entry.Role)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(NewUserRequest.Role)))
                .MaximumLength(1)
                .WithMessage(ValidatorMessages.MaxLength(nameof(NewUserRequest.Role), 1))
                .Must(r => r == "A" || r == "M" || r == "R")
                .WithMessage(ValidatorMessages.RoleError());
        }
    }
}
