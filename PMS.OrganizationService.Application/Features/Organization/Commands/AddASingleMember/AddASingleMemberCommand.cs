using MediatR;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddASingleMember;

public record AddASingleMemberCommand(
    Guid OrganizationId,
    string MemberEmail,
    bool Admin = false
    ) : IRequest<BaseResponse>;