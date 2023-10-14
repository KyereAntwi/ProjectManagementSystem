namespace PMS.OrganizationService.Domain.Entities;

public class Address
{
    public Guid OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }
    
    public string Address1 { get; private set; }
    public string? Address2 { get; private set; }
    public string? Address3 { get; private set; }
    public string Street { get; private set; }
    public string Region { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public string Telephone { get; private set; }

    public Address(Guid organizationId, string address1, string street, string region, string country, string zipCode, string telephone,
        string? address2 = null, string? address3 = null)
    {
        OrganizationId = organizationId;
        Address1 = address1;
        Address2 = address2;
        Address3 = address3;
        Street = street;
        Region = region;
        Country = country;
        ZipCode = zipCode;
        Telephone = telephone;
    }
}