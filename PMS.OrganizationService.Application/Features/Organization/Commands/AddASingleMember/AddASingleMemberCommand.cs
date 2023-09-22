using MediatR;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddASingleMember;

public record AddASingleMemberCommand(
    string OrganizationId,
    string MemberEmail,
    bool Admin
    ) : IRequest<BaseResponse>;