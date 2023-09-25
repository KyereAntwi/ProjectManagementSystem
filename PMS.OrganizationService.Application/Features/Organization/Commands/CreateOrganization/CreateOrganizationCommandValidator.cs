using FluentValidation;
using PMS.OrganizationService.Application.Contracts.Persistence;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.CreateOrganization;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    private readonly IOrganizationRepository _organizationRepository;

    public CreateOrganizationCommandValidator(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
        
        RuleFor(o => o.Title)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(o => o.CreatedBy)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(o => o)
            .MustAsync(TitleTaken).WithMessage("Title already taken. Title property must be unique");
    }

    private async Task<bool> TitleTaken(CreateOrganizationCommand command, CancellationToken token)
    {
        var existingTitle = await _organizationRepository.TitleAlreadyTakenAsync(command.Title);
        return existingTitle;
    }
}