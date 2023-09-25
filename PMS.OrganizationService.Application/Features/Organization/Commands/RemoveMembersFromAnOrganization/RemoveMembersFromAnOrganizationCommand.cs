using MediatR;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.RemoveMembersFromAnOrganization;

public record RemoveMembersFromAnOrganizationCommand(
    List<string> Usernames,
    Guid OrganizationId
    ) : IRequest<BaseResponse>;