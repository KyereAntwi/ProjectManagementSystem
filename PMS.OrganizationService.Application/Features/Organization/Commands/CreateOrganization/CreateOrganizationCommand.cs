using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.CreateOrganization;

public record CreateOrganizationCommand(
    string Title,
    string Description,
    string CreatedBy,
    IFormFile? Logo,
    IFormFile? Banner
    ) : IRequest<BaseResponse>;