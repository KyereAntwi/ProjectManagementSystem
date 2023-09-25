using MediatR;
using PMS.Contracts.Responses;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddMultipleMembers;

public class AddMultipleMembersCommandHandler : IRequestHandler<AddMultipleMembersCommand, BaseResponse>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMemberRepository _asyncRepository;

    public AddMultipleMembersCommandHandler(IOrganizationRepository organizationRepository, 
                                    IMemberRepository asyncRepository)
    {
        _organizationRepository = organizationRepository;
        _asyncRepository = asyncRepository;
    }
    
    public async Task<BaseResponse> Handle(AddMultipleMembersCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddMultipleMembersCommandValidator(_organizationRepository);
        var response = new BaseResponse();
        
        var validationErrors = await validator.ValidateAsync(request, cancellationToken);
        
        if (validationErrors.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Request to add members failed";
            response.ValidationErrors = new List<string>();
            foreach (var error in validationErrors.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            response.StatusCode = 400;
            return response;
        }

        List<Member> members = new();

        var organization = await _organizationRepository.GetByIdAsync(request.OrganizationId);

        foreach (var username in request.Members)
        {
            var member = new Member()
            {
                Admin = false,
                TimeStamp = DateTime.Now,
                MemberEmail = username
            };
            
            member.Organizations.Add(organization);
            
            members.Add(member);
        }
        
        var usernames = await _asyncRepository.AddMultiple(members);
        response.StatusCode = 201;
        response.Message = "Operation was successful";
        response.Data = usernames;
            
        // TODO - Send a message to the Activities Service for registering this activity
        
        return response;
    }
}