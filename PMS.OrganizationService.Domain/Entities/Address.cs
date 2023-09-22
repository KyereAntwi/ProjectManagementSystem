namespace PMS.OrganizationService.Domain.Entities;

public class Address
{
    public string OrganizationId { get; set; } = String.Empty;
    public string Address1 { get; set; } = String.Empty;
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string Street { get; set; } = String.Empty;
    public string Region { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public string ZipCode { get; set; } = String.Empty;
}