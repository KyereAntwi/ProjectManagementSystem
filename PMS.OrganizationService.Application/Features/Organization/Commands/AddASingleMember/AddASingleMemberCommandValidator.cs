using FluentValidation;
using PMS.OrganizationService.Application.Contracts.Persistence;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddASingleMember;

public class AddASingleMemberCommandValidator : AbstractValidator<AddASingleMemberCommand>
{
    private readonly IOrganizationRepository _organizationRepository;

    public AddASingleMemberCommandValidator(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
        
        RuleFor(m => m.MemberEmail)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(m => m.OrganizationId)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(m => m)
            .MustAsync(OrganizationExist).WithMessage("Specified organization Id does not exist");
    }

    private async Task<bool> OrganizationExist(AddASingleMemberCommand command, CancellationToken token)
    {
        return await _organizationRepository.TitleAlreadyTaken(command.OrganizationId);
    }
}