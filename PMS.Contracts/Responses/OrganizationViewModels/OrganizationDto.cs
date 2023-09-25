namespace PMS.Contracts.Responses.OrganizationViewModels;

public class OrganizationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Uri? LogoUrl { get; set; }
    public Uri? BannerUrl { get; set; }
    public bool Active { get; set; }
}