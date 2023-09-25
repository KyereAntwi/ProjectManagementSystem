using MediatR;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Application.Exceptions;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.DeleteOrganization;

public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand>
{
    private readonly IOrganizationRepository _asyncRepository;

    public DeleteOrganizationCommandHandler(IOrganizationRepository asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }
    
    public async Task Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await _asyncRepository.GetByIdAsync(request.Id);

        if (organization is null)
            throw new NotFoundException("Specified organization does not exist", typeof(Domain.Entities.Organization));

        await _asyncRepository.DeleteAsync(organization);
    }
}