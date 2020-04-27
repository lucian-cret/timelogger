using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TimeLogger.IntegrationTests
{
    public class ProjectsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProjectsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ProjectsList_ReturnsViewWithProjectsList()
        {
            var response = await _client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Test Project 1", responseString);
            Assert.Contains("Test Project 1 Description", responseString);
        }
    }
}
