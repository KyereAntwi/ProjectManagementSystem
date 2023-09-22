using AutoMapper;
using PMS.OrganizationService.Application.Features.Organization.Commands.AddASingleMember;
using PMS.OrganizationService.Application.Features.Organization.Commands.CreateOrganization;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateOrganizationCommand, Organization>();
        CreateMap<AddASingleMemberCommand, Member>();
    }
}