using MediatR;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.DeleteOrganization;

public record DeleteOrganizationCommand(
    Guid Id
    ) : IRequest;