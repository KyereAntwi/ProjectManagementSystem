using AutoMapper;
using MediatR;
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
    private readonly IAsyncRepository<Member> _asyncRepository;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, 
                                            IMapper mapper,
                                            IFileService fileService,
                                            IAsyncRepository<Member> asyncRepository)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
        _fileService = fileService;
        _asyncRepository = asyncRepository;
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
        }

        if (!response.Success)
            return response;
        
        var organization = _mapper.Map<Domain.Entities.Organization>(request);
            
        try
        {
            if (request.Banner is not null)
            {
                var bannerUrl = await _fileService.UploadImage(request.Banner);
                organization.BannerUrl = bannerUrl;
            }

            if (request.Logo is not null)
            {
                var logoUrl = await _fileService.UploadImage(request.Logo);
                organization.LogoUrl = logoUrl;
            }
            
            organization.TimeStamp = DateTime.Now;
            organization.Active = true;
            
            var newOrganization = await _organizationRepository.AddAsync(organization);

            await _asyncRepository.AddAsync(new Member()
            {
                OrganizationId = newOrganization.Id,
                MemberEmail = request.CreatedBy,
                TimeStamp = DateTime.Now,
                Owner = true,
                Admin = true
            });
            
            // TODO - Send a message to the Email service for email to be sent to owner or Admin for successful creation of organization and related tips
            // TODO - Send a message to the Activities Service for registering this activity
            
            response.Data = newOrganization.Id;
            response.Message = "organization created successfully";
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = "Creating organization failed";
        }
        
        return response;
    }
}