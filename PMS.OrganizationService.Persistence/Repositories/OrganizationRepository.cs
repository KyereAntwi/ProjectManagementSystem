using Microsoft.EntityFrameworkCore;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;
using PMS.OrganizationService.Persistence.Data;

namespace PMS.OrganizationService.Persistence.Repositories;

public class OrganizationRepository : AsyncRepository<Organization>, IOrganizationRepository
{
    private readonly OrganizationServiceDbContext _dbContext;

    public OrganizationRepository(OrganizationServiceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Organization?> GetByIdAsync(Guid id) => await _dbContext.Organizations.FindAsync(id);

    public async Task<Organization?> GetDetailsAsync(Guid id) => await _dbContext
        .Organizations
        .Include(o => o.Address)
        .Include(o => o.Members)
        .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IReadOnlyList<Organization>> ListAllAsync() => await _dbContext.Organizations.ToListAsync();

    public async Task<IReadOnlyList<Organization>> GetPagedResponseAsync(int page, int size)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> TitleAlreadyTakenAsync(string title)
    {
        var organization =
            await _dbContext.Organizations.FirstOrDefaultAsync(o => o.Title.ToLower().Contains(title.ToLower()));

        if (organization is null)
        {
            return false;
        }

        return true;
    }
}