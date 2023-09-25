using Microsoft.EntityFrameworkCore;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Persistence.Data;

public class OrganizationServiceDbContext : DbContext
{
    public OrganizationServiceDbContext(DbContextOptions<OrganizationServiceDbContext> options) : base(options)
    {
    }

    public DbSet<Organization> Organizations { get; set; } = default!;
    public DbSet<Member> Members { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;
}