using FluentValidation;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddASingleMember;

public class AddASingleMemberCommandValidator : AbstractValidator<AddASingleMemberCommand>
{
    public AddASingleMemberCommandValidator()
    {
        RuleFor(m => m.MemberEmail)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(m => m.OrganizationId)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
    }
}