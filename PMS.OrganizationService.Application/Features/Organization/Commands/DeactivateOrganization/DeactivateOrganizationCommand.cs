using MediatR;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.DeactivateOrganization;

public record DeactivateOrganizationCommand(Guid OrganizationId) : IRequest;