using MediatR;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddMultipleMembers;

public record AddMultipleMembersCommand(
    IEnumerable<string> Members,
    Guid OrganizationId
    ) : IRequest<BaseResponse>;