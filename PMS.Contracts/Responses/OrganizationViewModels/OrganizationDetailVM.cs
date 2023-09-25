namespace PMS.Contracts.Responses.OrganizationViewModels;

public class OrganizationDetailVM
{
    public OrganizationDto Summary { get; set; }
    public OrganizationAddressDto Address { get; set; }
    public IEnumerable<string> Members { get; set; }
    public IEnumerable<string> Admins { get; set; }
}