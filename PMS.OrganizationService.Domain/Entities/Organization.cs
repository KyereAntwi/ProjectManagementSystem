namespace PMS.OrganizationService.Domain.Entities;

public class Organization
{
    public string Id { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; }
    public Uri? LogoUrl { get; set; }
    public Uri? BannerUrl { get; set; }
    public bool Active { get; set; }
}