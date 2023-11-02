using AutoMapper;
using Moq;
using Shouldly;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Application.Features.Categories.Commands.CreateCategory;
using TicketManagement.Application.MappingProfiles;
using TicketManagement.Application.UnitTests.Mocks;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.UnitTests.Categories.Commands;

public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandlerTests()
    {
        _mockCategoryRepository = CategoryRepositoryMock.GetCategoryRepository();
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        _mapper = configurationProvider.CreateMapper();
        
    }

    [Fact]
    public async Task CreateValidCategoryTests()
    {
        var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);
        
        await handler.Handle(new CreateCategoryCommand(){Name = "New Category"}, CancellationToken.None);

        var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
        
        allCategories.Count.ShouldBe(5);
    }
}