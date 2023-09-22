using Microsoft.AspNetCore.Http;

namespace PMS.OrganizationService.Application.Contracts.Infrustracture;

public interface IFileService
{
    Task<Uri> UploadImage(IFormFile file);
}