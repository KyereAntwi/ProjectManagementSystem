using AutoMapper;
using MediatR;
using PMS.Contracts.Responses;
using PMS.OrganizationService.Application.Contracts.Infrustracture;
using PMS.OrganizationService.Application.Contracts.Persistence;

namespace PMS.OrganizationService.Application.Features.Organization.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, BaseResponse>
{
    private readonly IOrganizationRepository _asyncRepository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateOrganizationCommandHandler(IOrganizationRepository asyncRepository, IMapper mapper, IFileService fileService)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
        _fileService = fileService;
    }
    
    public async Task<BaseResponse> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        
        var validator = new UpdateOrganizationCommandValidator(_asyncRepository);
        var validationErrors = await validator.ValidateAsync(request, cancellationToken);
        
        if (validationErrors.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Request to update organization information failed";
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

        var organization = Domain.Entities.Organization.Update(request.Id, request.Title!, request.Description!, loggoUrl,
            bannerUrl, request.Address1, request.Address2, request.Address3, request.Street!, request.Region!,
            request.Country!, request.ZipCode!, request.Telephone!);

        await _asyncRepository.UpdateAsync(organization);
        
        // TODO - Send a message to the Email service for email to be sent to owner or Admin for successful update of organization and related tips
        // TODO - Send a message to the Activities Service for registering this activity
            
        response.Data = organization.Id;
        response.Message = "organization update with address was successfully";

        return response;
    }
}