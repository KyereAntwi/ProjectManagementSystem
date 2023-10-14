using MediatR;
using PMS.Contracts.Responses;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddASingleMember;

public class AddASingleMemberCommandHandler : IRequestHandler<AddASingleMemberCommand, BaseResponse>
{
    private readonly IOrganizationRepository _asyncRepository;

    public AddASingleMemberCommandHandler(IOrganizationRepository asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }
    
    public async Task<BaseResponse> Handle(AddASingleMemberCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        
        var validator = new AddASingleMemberCommandValidator();
        var validationErrors = await validator.ValidateAsync(request, cancellationToken);
        
        if (validationErrors.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Request to add member failed";
            response.ValidationErrors = new List<string>();
            foreach (var error in validationErrors.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            response.StatusCode = 400;
        }

        var organization = await _asyncRepository.GetByIdAsync(request.OrganizationId);

        Domain.Entities.Organization.AddMembersToOrganization(organization!,
            new List<Member>() { Member.Create(request.MemberEmail, DateTime.UtcNow, request.Admin, false) });

        await _asyncRepository.SaveChanges();
        
        response.Success = true;
        response.Message = "Adding member was successful";
        response.StatusCode = 201;
            
        // TODO - Send a message to the Activities Service for registering this activity

        return response;
    }
}