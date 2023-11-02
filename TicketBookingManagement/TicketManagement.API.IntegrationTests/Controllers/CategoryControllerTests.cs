using System.Text.Json;
using Shouldly;
using TicketManagement.API.IntegrationTests.Base;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;

namespace TicketManagement.API.IntegrationTests.Controllers
{

    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CategoryListVm>>(responseString);

            result.ShouldBeOfType<List<CategoryListVm>>();
            result.ShouldNotBeEmpty();
        }
    }
}
