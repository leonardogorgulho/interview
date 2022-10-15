using FinDox.Domain.DataTransfer;
using FluentValidation;

namespace FinDox.Application.Validators
{
    public class UserEntryRequestValidator : AbstractValidator<UserEntryRequest>
    {
        public UserEntryRequestValidator()
        {
            RuleFor(entry => entry.Name)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(UserEntryRequest.Name)))
                .MaximumLength(150)
                .WithMessage(ValidatorMessages.MaxLength(nameof(UserEntryRequest.Name), 150));

            RuleFor(entry => entry.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(UserEntryRequest.Password)))
                .Must(p => p.Length == 32)
                .WithMessage(ValidatorMessages.ExactLength(nameof(UserEntryRequest.Password), 32));

            RuleFor(entry => entry.Login)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(UserEntryRequest.Login)))
                .MaximumLength(30)
                .WithMessage(ValidatorMessages.MaxLength(nameof(UserEntryRequest.Login), 30));

            RuleFor(entry => entry.Role)
                .NotEmpty()
                .WithMessage(ValidatorMessages.RequiredField(nameof(UserEntryRequest.Role)))
                .MaximumLength(1)
                .WithMessage(ValidatorMessages.MaxLength(nameof(UserEntryRequest.Role), 1))
                .Must(r => r == "A" || r == "M" || r == "R")
                .WithMessage(ValidatorMessages.RoleError());
        }
    }
}
