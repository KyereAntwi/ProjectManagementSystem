using AutoMapper;
using MediatR;
using PMS.Contracts.Responses;
using PMS.Contracts.Responses.OrganizationViewModels;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Application.Exceptions;

namespace PMS.OrganizationService.Application.Features.Organization.Queries.GetOrganizationDetail;

public class GetOrganizationDetailQueryHandler : IRequestHandler<GetOrganizationDetailQuery, BaseResponse>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;
    private readonly IMemberRepository _memberRepository;

    public GetOrganizationDetailQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper, IMemberRepository memberRepository)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
        _memberRepository = memberRepository;
    }
    
    public async Task<BaseResponse> Handle(GetOrganizationDetailQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetDetailsAsync(request.Id);

        if (organization is null)
            throw new NotFoundException("Specified organization does not exist", typeof(Domain.Entities.Organization));

        var response = new BaseResponse();
        var vm = new OrganizationDetailVM();
        
        var organizationDto = _mapper.Map<OrganizationDto>(organization);
        vm.Summary = organizationDto;

        if (organization.Address is not null)
        {
            var addressDto = _mapper.Map<OrganizationAddressDto>(organization.Address);
            vm.Address = addressDto;
        }

        vm.Members = new List<string>();
        if (organization.Members.Any())
        {
            vm.Members = organization.Members.Select(m => m.MemberEmail).ToList();
        }

        var administrators = await _memberRepository.Administrators(request.Id);
        
        vm.Admins = new List<string>();

        if (administrators.Any())
        {
            vm.Admins = administrators.Select(a => a.MemberEmail).ToList();
        }

        response.Data = vm;

        return response;
    }
}