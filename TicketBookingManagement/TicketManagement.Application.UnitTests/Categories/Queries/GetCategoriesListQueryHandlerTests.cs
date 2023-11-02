using AutoMapper;
using Moq;
using Shouldly;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using TicketManagement.Application.MappingProfiles;
using TicketManagement.Application.UnitTests.Mocks;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.UnitTests.Categories.Queries;

public class GetCategoriesListQueryHandlerTests
{
    private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesListQueryHandlerTests()
    {
        _mockCategoryRepository = CategoryRepositoryMock.GetCategoryRepository();
        var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task GetCategoriesListTests()
    {
        var handler = new GetCategoriesListQueryHandler(_mapper, _mockCategoryRepository.Object);

        var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<CategoryListVm>>();
        
        result.Count.ShouldBe(4);
    }
}