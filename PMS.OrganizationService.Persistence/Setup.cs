using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.OrganizationService.Application.Contracts.Infrustracture;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Persistence.Data;
using PMS.OrganizationService.Persistence.Repositories;
using PMS.OrganizationService.Persistence.Services;

namespace PMS.OrganizationService.Persistence;

public static class Setup
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        services.AddDbContext<OrganizationServiceDbContext>(options =>
            options.UseSqlite(connectionString, b => b.MigrationsAssembly("")));
        
        services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        services.AddTransient<IFileService, FileService>();
        
        return services;
    }
}