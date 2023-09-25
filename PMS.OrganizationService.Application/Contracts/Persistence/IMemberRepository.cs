using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Contracts.Persistence;

public interface IMemberRepository : IAsyncRepository<Member>
{
    public Task<IReadOnlyList<string>> AddMultiple(IEnumerable<Member> members);
    public Task DeleteMultiple(Guid organizationId, List<string> usernames);
    public Task<IReadOnlyList<Member>> Administrators(Guid organizationId);
}