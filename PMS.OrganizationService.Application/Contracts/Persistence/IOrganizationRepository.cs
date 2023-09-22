using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Contracts.Persistence;

public interface IOrganizationRepository : IAsyncRepository<Organization>
{
    Task<Organization> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Organization>> ListAllAsync();
    Task<IReadOnlyList<Organization>> GetPagedResponseAsync(int page, int size);
    Task<int> Count();
    Task<bool> TitleAlreadyTaken(string title);
}