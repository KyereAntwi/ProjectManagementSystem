using FluentValidation;
using PMS.OrganizationService.Application.Contracts.Persistence;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddMultipleMembers;

public class AddMultipleMembersCommandValidator : AbstractValidator<AddMultipleMembersCommand>
{
    private readonly IOrganizationRepository _organizationRepository;

    public AddMultipleMembersCommandValidator(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
        
        RuleFor(m => m.OrganizationId)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(m => m)
            .MustAsync(OrganizationExist).WithMessage("Specified organization Id does not exist");
    }

    private async Task<bool> OrganizationExist(AddMultipleMembersCommand command, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(command.OrganizationId);

        if (organization is not null)
            return true;
        return false;
    }
}