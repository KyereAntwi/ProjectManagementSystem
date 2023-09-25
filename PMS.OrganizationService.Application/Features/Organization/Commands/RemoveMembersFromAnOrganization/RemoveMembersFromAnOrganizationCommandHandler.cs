using MediatR;
using PMS.Contracts.Responses;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Application.Exceptions;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.RemoveMembersFromAnOrganization;

public class RemoveMembersFromAnOrganizationCommandHandler : IRequestHandler<RemoveMembersFromAnOrganizationCommand, BaseResponse>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IOrganizationRepository _organizationRepository;

    public RemoveMembersFromAnOrganizationCommandHandler(IMemberRepository memberRepository, IOrganizationRepository organizationRepository)
    {
        _memberRepository = memberRepository;
        _organizationRepository = organizationRepository;
    }
    
    public async Task<BaseResponse> Handle(RemoveMembersFromAnOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.OrganizationId);

        if (organization is null)
        {
            throw new NotFoundException("The specified organization does not exist", typeof(Member));
        }

        await _memberRepository.DeleteMultiple(request.OrganizationId, request.Usernames);

        return new BaseResponse()
        {
            StatusCode = 200,
            Message = "Delete operation was successful"
        };
    }
}