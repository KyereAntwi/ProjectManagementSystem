using MediatR;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddMultipleMembers;

public record AddMultipleMembersCommand(
    IEnumerable<string> Members,
    string OrganizationId
    ) : IRequest<BaseResponse>;