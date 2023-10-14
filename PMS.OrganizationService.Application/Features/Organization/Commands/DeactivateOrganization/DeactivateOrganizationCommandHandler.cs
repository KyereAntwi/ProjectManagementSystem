using MediatR;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Application.Exceptions;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.DeactivateOrganization;

public class DeactivateOrganizationCommandHandler : IRequestHandler<DeactivateOrganizationCommand>
{
    private readonly IOrganizationRepository _asyncRepository;

    public DeactivateOrganizationCommandHandler(IOrganizationRepository asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }
    
    public async Task Handle(DeactivateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await _asyncRepository.GetByIdAsync(request.OrganizationId);

        if (organization is null)
            throw new NotFoundException("Specified organization does not exist", typeof(Domain.Entities.Organization));

        var updatedOrganization = Domain.Entities.Organization.Deactivate(organization);

        await _asyncRepository.UpdateAsync(updatedOrganization);
    }
}