using FluentValidation;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddMultipleMembers;

public class AddMultipleMembersCommandValidator : AbstractValidator<AddMultipleMembersCommand>
{
    public AddMultipleMembersCommandValidator()
    {
    }
}