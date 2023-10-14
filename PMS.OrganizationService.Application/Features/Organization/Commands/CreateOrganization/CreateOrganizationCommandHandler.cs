using AutoMapper;
using MediatR;
using MediatR.Wrappers;
using PMS.Contracts.Responses;
using PMS.OrganizationService.Application.Contracts.Infrustracture;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Domain.Entities;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, BaseResponse>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, 
                                            IMapper mapper,
                                            IFileService fileService)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
        _fileService = fileService;
    }
    
    public async Task<BaseResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        
        var validator = new CreateOrganizationCommandValidator(_organizationRepository);
        var validationErrors = await validator.ValidateAsync(request, cancellationToken);
        
        if (validationErrors.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Request to create organization failed";
            response.ValidationErrors = new List<string>();
            foreach (var error in validationErrors.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        Uri? bannerUrl = null;
        Uri? loggoUrl = null;
        
        if (request.Banner is not null)
        {
            bannerUrl = await _fileService.UploadImage(request.Banner);
        }

        if (request.Logo is not null)
        {
            loggoUrl = await _fileService.UploadImage(request.Logo);
        }

        var organization = Domain.Entities.Organization.Create(request.Title, request.Description, request.CreatedBy,
            loggoUrl, bannerUrl);
        
        var newOrganization = await _organizationRepository.AddAsync(organization);
            
        // TODO - Send a message to the Email service for email to be sent to owner or Admin for successful creation of organization and related tips
        // TODO - Send a message to the Activities Service for registering this activity
            
        response.Data = newOrganization.Id;
        response.Message = "organization created successfully";
        
        return response;
    }
}