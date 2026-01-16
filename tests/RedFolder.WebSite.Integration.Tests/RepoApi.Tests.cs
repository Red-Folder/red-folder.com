using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using RedFolder.Models.Api;
using System.Collections.Generic;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    public class RepoApiTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public RepoApiTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task Get_RepoApi_ReturnsSuccessAndRepoList()
        {
            var response = await _httpClientFixture.Client.GetAsync("/api/repo");
            response.EnsureSuccessStatusCode();

            var repos = await response.Content.ReadFromJsonAsync<List<Repo>>();
            
            Assert.NotNull(repos);
            Assert.IsType<List<Repo>>(repos);
        }

        [Fact]
        public async Task Get_RepoApi_ReturnsJsonContentType()
        {
            var response = await _httpClientFixture.Client.GetAsync("/api/repo");
            response.EnsureSuccessStatusCode();

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }
    }
}
