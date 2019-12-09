using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TimeLogger.IntegrationTests
{
    public class TimeLogsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TimeLogsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task TimeLogsList_ProjectId1_ReturnsViewWithTimeLogsListForProject1()
        {
            var response = await _client.GetAsync("/TimeLogs/TimeLogsList/?projectId=1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Write unit tests", responseString);
            Assert.Contains("Change framework version", responseString);
        }

        [Fact]
        public async Task TimeLogsList_NoProjectIdInQueryString_ReturnsViewWithProjectsList()
        {
            var response = await _client.GetAsync("/TimeLogs/TimeLogsList");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Test Project 1", responseString);
            Assert.Contains("Test Project 1 Description", responseString);
        }

        [Fact]
        public async Task LogTimeGet_ProjectId1_ReturnsViewWithTimeLogsListForProject1()
        {
            var response = await _client.GetAsync("/TimeLogs/LogTime/?projectId=1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Worked hours", responseString);
            Assert.Contains("Description", responseString);
        }

        [Fact]
        public async Task LogTimeGet_NoProjectIdInQueryString_ReturnsViewWithProjectsList()
        {
            var response = await _client.GetAsync("/TimeLogs/LogTime");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Test Project 1", responseString);
            Assert.Contains("Test Project 1 Description", responseString);
        }
    }
}
