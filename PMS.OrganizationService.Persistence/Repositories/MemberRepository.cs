using Microsoft.EntityFrameworkCore;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;
using PMS.OrganizationService.Persistence.Data;

namespace PMS.OrganizationService.Persistence.Repositories;

public class MemberRepository: AsyncRepository<Member>, IMemberRepository
{
    private readonly OrganizationServiceDbContext _dbContext;

    public MemberRepository(OrganizationServiceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<string>> AddMultiple(IEnumerable<Member> members)
    {
        await _dbContext.Members.AddRangeAsync(members);
        await _dbContext.SaveChangesAsync();
        return members.Select(m => m.MemberEmail).ToList();
    }

    public async Task DeleteMultiple(Guid organizationId, List<string> usernames)
    {
        var members = new List<Member>();
        
        var organization = await _dbContext.Organizations
            .FirstOrDefaultAsync(o => o.Id == organizationId);

        foreach (var username in usernames)
        {
            var existingMember = await _dbContext.Members
                .FirstOrDefaultAsync(m => m.MemberEmail == username);

            if (existingMember is not null)
            {
                members.Add(existingMember);
            }
        }

        foreach (var member in members)
        {
            organization!.Members.Remove(member);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Member>> Administrators(Guid organizationId)
    {
        var organization = await _dbContext.Organizations
            .Include(a => a.Members)
            .FirstOrDefaultAsync(o => o.Id == organizationId);

        var admins = new List<Member>();

        foreach (var member in organization!.Members)
        {
            if (member.Admin)
            {
                admins.Add(member);
            }
        }

        return admins;
    }
}