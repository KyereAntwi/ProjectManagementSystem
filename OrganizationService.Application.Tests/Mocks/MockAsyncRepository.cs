using Moq;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;

namespace OrganizationService.Application.Tests.Mocks;

public static class MockAsyncRepository
{
    public static Mock<IOrganizationRepository> GetOrganizationAsyncRepository()
    {
        var OrganizationList = new List<Organization>()
        {
            new Organization()
            {
                Id = new Guid("2245fe4a-d402-451c-b9ed-9c1a04247482"),
                Title = "Organization 1"
            }
        };
        
        var mockRepo = new Mock<IOrganizationRepository>();
        
        // mock AddAsync - return Organization
        mockRepo.Setup(r =>
            r.AddAsync(It.IsAny<Organization>())).ReturnsAsync((Organization organization) =>
        {
            organization.Id = new Guid("2245fe4a-d402-451c-b9ed-9c1a04247482");
            OrganizationList.Add(organization);
            
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
            var response = OrganizationList.FirstOrDefault(o => o.Id == id);
            return response;
        });
        
        // mock GetDetailsAsync - return Organization
        mockRepo.Setup(r =>
            r.GetDetailsAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var response = OrganizationList.FirstOrDefault(o => o.Id == id);
            return response;
        });
        
        // mock TitleAlreadyTakenAsync - return bool
        mockRepo.Setup(r =>
            r.TitleAlreadyTakenAsync(It.IsAny<string>())).ReturnsAsync((string title) =>
        {
            var response = OrganizationList.FirstOrDefault(o => o.Title.ToLower().Contains(title.ToLower()));
            if (response is null)
            {
                return true;
            }

            return false;
        });

        return mockRepo;
    }
}