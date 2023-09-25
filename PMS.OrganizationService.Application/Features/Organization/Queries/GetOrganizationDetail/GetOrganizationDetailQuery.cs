using MediatR;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Queries.GetOrganizationDetail;

public record GetOrganizationDetailQuery(
    Guid Id
    ) : IRequest<BaseResponse>;