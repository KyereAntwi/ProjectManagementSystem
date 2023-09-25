using Microsoft.Extensions.DependencyInjection;
using PMS.OrganizationService.Application.Profiles;

namespace PMS.OrganizationService.Application;

public static class StartUp
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<MappingProfiles>());
        
        return services;
    }
}