using FinDox.Domain.DataTransfer;
using FluentValidation;

namespace FinDox.Application.Validators
{
    public class GroupRequestValidator : AbstractValidator<GroupRequest>
    {
        public GroupRequestValidator()
        {
            RuleFor(g => g.Name)
                .NotNull()
                .WithMessage(ValidatorMessages.RequiredField(nameof(GroupRequest.Name)))
                .MaximumLength(50)
                .WithMessage(ValidatorMessages.MaxLength(nameof(GroupRequest.Name), 50));
        }
    }
}
