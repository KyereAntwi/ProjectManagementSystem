using AutoMapper;
using FluentAssertions;
using Moq;
using OrganizationService.Application.Tests.Mocks;
using PMS.OrganizationService.Application.Contracts.Persistence;
using PMS.OrganizationService.Application.Features.Organization.Commands.CreateOrganization;
using PMS.OrganizationService.Application.Features.Organization.Commands.DeleteOrganization;
using PMS.OrganizationService.Application.Features.Organization.Commands.UpdateOrganization;
using PMS.OrganizationService.Application.Profiles;

namespace OrganizationService.Application.Tests.Features;

public class OrganizationCommandHandlersTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IOrganizationRepository> _mock;
    
    public OrganizationCommandHandlersTests()
    {
        _mock = MockAsyncRepository.GetOrganizationAsyncRepository();
        
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfiles>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateOrganizationCommand_Handler_BaseResponse()
    {
        var handler = new CreateOrganizationCommandHandler(_mock.Object, _mapper, null);
            
        var command = new CreateOrganizationCommand(
            "Organization 2",
            "This is the description for organization 1",
            "user@example.com",
            null,
            null
            );
        
        var result = 
            await handler.Handle(command, CancellationToken.None);

        result.Data.Should().BeOfType<Guid>();
    }

    [Fact]
    public async Task UpdateOrganizationCommand_Handler_BaseResponse()
    {
        var handler = new UpdateOrganizationCommandHandler(_mock.Object, _mapper, null);

        var command = new UpdateOrganizationCommand(
            new Guid("2245fe4a-d402-451c-b9ed-9c1a04247482"),
            "This the updated description",
            null,
            null,
            "Address 1",
            "Address 2",
            null,
            "Street",
            "Region",
            "Country",
            "0000",
            "+2331234567899"
            );

        var result = await handler.Handle(command, CancellationToken.None);

        result.Data.Should().BeOfType<Guid>();
    }

    [Fact]
    public async Task DeleteOrganizationCommand_Handler_Nothing()
    {
        var handler = new DeleteOrganizationCommandHandler(_mock.Object);

        var command = new DeleteOrganizationCommand(new Guid("2245fe4a-d402-451c-b9ed-9c1a04247482"));

        await handler.Handle(command, CancellationToken.None);
    }
}