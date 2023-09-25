namespace PMS.OrganizationService.Domain.Entities;

public class Member
{
    public Guid Id { get; set; }
    public string MemberEmail { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public bool Admin { get; set; }
    public bool Owner { get; set; }
    public ICollection<Organization> Organizations { get; set; } = default!;
}