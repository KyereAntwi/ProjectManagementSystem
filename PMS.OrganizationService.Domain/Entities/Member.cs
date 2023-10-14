namespace PMS.OrganizationService.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; }
    public string MemberEmail { get; private set; }
    public DateTime TimeStamp { get; private set; }
    public bool Admin { get; private set; }
    public bool Owner { get; private set; }
    public ICollection<Organization> Organizations { get; set; } = default!;

    private Member()
    {
    }

    private Member(string memberEmail, DateTime timeStamp, bool admin, bool owner)
    {
        TimeStamp = timeStamp;
        Admin = admin;
        Owner = owner;
        MemberEmail = memberEmail;
    }

    public static Member Create(string memberEmail, DateTime timeStamp, bool admin, bool owner)
    {
        return new Member(memberEmail, timeStamp, admin, owner);
    }
}