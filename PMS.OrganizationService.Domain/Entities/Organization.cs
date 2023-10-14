namespace PMS.OrganizationService.Domain.Entities;

public class Organization
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime TimeStamp { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Uri? LogoUrl { get; private set; }
    public Uri? BannerUrl { get; private set; }
    public bool Active { get; private set; }

    public Address? Address { get; private set; }

    public ICollection<Member> Members { get; private set; } = default!;

    private Organization()
    { }
    
    private Organization(string title, string description)
    {
        Title = title;
        Description = description;
    }

    // create a new organization
    public static Organization Create(
        string title, string description, string createdBy, Uri? logo = null, Uri? bannerUrl = null)
    {
        var organization =
            new Organization(title, description);
        
        if (bannerUrl is not null)
        {
            organization.BannerUrl = bannerUrl;
        }

        if (logo is not null)
        {
            organization.LogoUrl = logo;
        }
        
        organization.TimeStamp = DateTime.UtcNow;
        organization.UpdatedAt = DateTime.UtcNow;
        organization.Active = true;

        var adminMember = Member.Create(createdBy, DateTime.UtcNow, true, true);
        
        organization.Members = new List<Member>();    
        organization.Members.Add(adminMember);

        return organization;
    }
    
    // deactivate an existing organization
    public static Organization Deactivate(Organization organization)
    {
        organization.Active = false;
        organization.UpdatedAt = DateTime.UtcNow;
        return organization;
    }
    
    // update full existing organization
    public static Organization Update(Guid id, string title, string description, Uri? logoUrl, Uri? bannerUrl, 
        string? address1, string? address2, string? address3, string street, 
        string region, string country, string zipCode, string telephone)
    {
        var organization = new Organization(title, description)
        {
            Id = id,
            UpdatedAt = DateTime.UtcNow
        };
        
        if (bannerUrl is not null)
        {
            organization.BannerUrl = bannerUrl;
        }

        if (logoUrl is not null)
        {
            organization.LogoUrl = logoUrl;
        }

        if (string.IsNullOrWhiteSpace(address1))
        {
            var address = new Address(id, address1!, street, region, country, zipCode, telephone,
                string.IsNullOrWhiteSpace(address2) ? String.Empty : address2,
                string.IsNullOrWhiteSpace(address3) ? String.Empty : address3);

            organization.Address = address;
        }

        return organization;
    }
    
    // add a members to organization
    public static void AddMembersToOrganization(Organization organization, ICollection<Member> members)
    {
        foreach (var member in members)
        {
            organization.Members.Add(member);
        }
    }
}