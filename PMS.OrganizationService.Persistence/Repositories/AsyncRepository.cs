using Microsoft.EntityFrameworkCore;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Persistence.Data;

namespace PMS.OrganizationService.Persistence.Repositories;

public class AsyncRepository<T> : IAsyncRepository<T> where T : class
{
    private readonly OrganizationServiceDbContext _dbContext;

    public AsyncRepository(OrganizationServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}