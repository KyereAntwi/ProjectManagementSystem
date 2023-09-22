namespace PMS.OrganizationService.Domain.Entities;

public class Member
{
    public string Id { get; set; } = String.Empty;
    public string OrganizationId { get; set; } = String.Empty;
    public string MemberEmail { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public bool Admin { get; set; } = false;
    public bool Owner { get; set; } = false;
}