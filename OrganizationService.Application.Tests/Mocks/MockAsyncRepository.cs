using Moq;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;

namespace OrganizationService.Application.Tests.Mocks;

public static class MockAsyncRepository
{
    public static Mock<IOrganizationRepository> GetOrganizationAsyncRepository()
    {
        var organizationList = new List<Organization>();
        
        var mockRepo = new Mock<IOrganizationRepository>();
        
        // mock AddAsync - return Organization
        mockRepo.Setup(r =>
            r.AddAsync(It.IsAny<Organization>())).ReturnsAsync((Organization organization) =>
        {
            var newOrganization =
                Organization.Create("Sample Title", "Sample organization description", "user@example.com");
            
            organizationList.Add(organization);
            
            return organization;
        });
        
        // mock UpdateAsync
        mockRepo.Setup(r =>
            r.UpdateAsync(It.IsAny<Organization>()));
        
        // mock remove
        mockRepo.Setup(r =>
            r.DeleteAsync(It.IsAny<Organization>()));
        
        // mock GetByIdAsync - return Organization
        mockRepo.Setup(r =>
            r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var response = organizationList.FirstOrDefault(o => o.Id == id);
            return response;
        });
        
        // mock GetDetailsAsync - return Organization
        mockRepo.Setup(r =>
            r.GetDetailsAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var response = organizationList.FirstOrDefault(o => o.Id == id);
            return response;
        });
        
        // mock TitleAlreadyTakenAsync - return bool
        mockRepo.Setup(r =>
            r.TitleAlreadyTakenAsync(It.IsAny<string>())).ReturnsAsync((string title) =>
        {
            var response = organizationList.FirstOrDefault(o => o.Title.ToLower().Contains(title.ToLower()));
            if (response is null)
            {
                return true;
            }

            return false;
        });

        return mockRepo;
    }
}