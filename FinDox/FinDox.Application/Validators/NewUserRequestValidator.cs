using FinDox.Domain.DataTransfer;
using FluentValidation;

namespace FinDox.Application.Validators
{
    public class NewUserRequestValidator : AbstractValidator<NewUserRequest>
    {
        public NewUserRequestValidator()
        {
            RuleFor(entry => entry.Name)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(NewUserRequest.Name)))
                .MaximumLength(150)
                .WithMessage(ValidatorMessages.MaxLength(nameof(NewUserRequest.Name), 150));

            RuleFor(entry => entry.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(NewUserRequest.Password)))
                .Must(p => p.Length == 32)
                .WithMessage(ValidatorMessages.ExactLength(nameof(NewUserRequest.Password), 32));

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
