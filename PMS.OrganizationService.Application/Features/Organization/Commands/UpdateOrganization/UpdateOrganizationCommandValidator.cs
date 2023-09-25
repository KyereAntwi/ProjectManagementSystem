using FluentValidation;
using PMS.OrganizationService.Application.Contracts.Persistence;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.UpdateOrganization;

public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    private readonly IOrganizationRepository _organizationRepository;

    public UpdateOrganizationCommandValidator(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
        
        RuleFor(o => o.Address1)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
        
        RuleFor(o => o.Address2)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
        
        RuleFor(o => o.Street)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
        
        RuleFor(o => o.Region)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
        
        RuleFor(o => o.Country)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
        
        RuleFor(o => o.ZipCode)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
        
        RuleFor(o => o.Telephone)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull()
            .MinimumLength(10).WithMessage("{PropertyName} must be at least 10 characters");

        RuleFor(o => o)
            .MustAsync(Exists).WithMessage("Organization specified does not exist");
    }
    
    private async Task<bool> Exists(UpdateOrganizationCommand command, CancellationToken token)
    {
        var existingOrganization = await _organizationRepository.GetByIdAsync(command.Id);
        return existingOrganization is null ? false : true;
    }
}