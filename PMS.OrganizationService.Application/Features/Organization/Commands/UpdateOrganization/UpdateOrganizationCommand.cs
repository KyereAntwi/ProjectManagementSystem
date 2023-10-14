using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Contracts.Responses;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.UpdateOrganization;

public record UpdateOrganizationCommand(
    Guid Id,
    string? Title,
    string? Description,
    IFormFile? Logo,
    IFormFile? Banner,
    string? Address1,
    string? Address2,
    string? Address3,
    string? Street,
    string? Region,
    string? Country,
    string? ZipCode,
    string? Telephone
    ) : IRequest<BaseResponse>;