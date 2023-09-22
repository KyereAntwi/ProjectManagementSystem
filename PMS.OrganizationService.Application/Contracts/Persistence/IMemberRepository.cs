using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Contracts.Persistence;

public interface IMemberRepository : IAsyncRepository<Member>
{
    public Task<IReadOnlyList<string>> AddMultiple(IEnumerable<Member> members);
}