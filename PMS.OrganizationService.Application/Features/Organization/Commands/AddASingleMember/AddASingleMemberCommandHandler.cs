using AutoMapper;
using MediatR;
using PMS.Contracts.Responses;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.AddASingleMember;

public class AddASingleMemberCommandHandler : IRequestHandler<AddASingleMemberCommand, BaseResponse>
{
    private readonly IAsyncRepository<Member> _asyncRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public AddASingleMemberCommandHandler(IAsyncRepository<Member> asyncRepository, 
        IOrganizationRepository organizationRepository,
        IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseResponse> Handle(AddASingleMemberCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddASingleMemberCommandValidator(_organizationRepository);
        var response = new BaseResponse();
        
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
        }
        
        if (!response.Success)
            return response;

        var member = _mapper.Map<Member>(request);

        try
        {
            var newMember = await _asyncRepository.AddAsync(member);
            response.Success = true;
            response.Message = "Adding member was successful";
            response.StatusCode = 201;
            response.Data = newMember.MemberEmail;
            
            // TODO - Send a message to the Activities Service for registering this activity
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = "Creating organization failed. Sorry! Something went wrong";
            response.StatusCode = 500;
        }

        return response;
    }
}