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
        }
        
        if (!response.Success)
            return response;

        List<Member> members = new();

        foreach (var username in request.Members)
        {
            members.Add(new Member()
            {
                OrganizationId = request.OrganizationId,
                MemberEmail = username
            });
        }

        try
        {
            var usernames = await _asyncRepository.AddMultiple(members);
            response.StatusCode = 201;
            response.Message = "Operation was successful";
            response.Data = usernames;
            
            // TODO - Send a message to the Activities Service for registering this activity
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = "Adding members failed. Sorry! Something went wrong";
            response.StatusCode = 500;
        }

        return response;
    }
}