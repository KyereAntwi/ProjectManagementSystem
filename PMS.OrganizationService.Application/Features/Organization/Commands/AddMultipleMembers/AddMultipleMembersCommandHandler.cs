using MediatR;
using PMS.Contracts.Responses;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Application.Exceptions;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddMultipleMembers;

public class AddMultipleMembersCommandHandler : IRequestHandler<AddMultipleMembersCommand, BaseResponse>
{
    private readonly IOrganizationRepository _organizationRepository;

    public AddMultipleMembersCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    
    public async Task<BaseResponse> Handle(AddMultipleMembersCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        List<Member> members = new();

        var organization = await _organizationRepository.GetByIdAsync(request.OrganizationId);
        if (organization is null)
            throw new NotFoundException("Selected organization was not found", nameof(Domain.Entities.Organization));

        foreach (var username in request.Members)
        {
            members.Add(Member.Create(username, DateTime.UtcNow, false, false));
        }
        
        Domain.Entities.Organization.AddMembersToOrganization(organization, members);
        await _organizationRepository.SaveChanges();
        
        response.StatusCode = 201;
        response.Message = "Operation was successful";
            
        // TODO - Send a message to the Activities Service for registering this activity
        
        return response;
    }
}