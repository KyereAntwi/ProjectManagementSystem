using Microsoft.AspNetCore.Http;
using PMS.OrganizationService.Application.Contracts.Infrustracture;

namespace PMS.OrganizationService.Persistence.Services;

public class FileService : IFileService
{
    public FileService()
    {
        
    }
    public async Task<Uri> UploadImage(IFormFile file)
    {
        throw new NotImplementedException();
    }
}