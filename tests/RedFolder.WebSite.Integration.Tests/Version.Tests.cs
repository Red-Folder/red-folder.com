using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using RedFolder.Models;
using System.Net;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    public class VersionTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public VersionTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task Get_Version_ReturnsSuccessOrNotFound()
        {
            var response = await _httpClientFixture.Client.GetAsync("/api/version");
            
            // In test environment, version.json may not exist, so we accept both 200 and 404
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotFound,
                $"Expected status code 200 or 404, but got {response.StatusCode}"
            );
        }

        [Fact]
        public async Task Get_Version_WhenAvailable_ReturnsVersionInfo()
        {
            var response = await _httpClientFixture.Client.GetAsync("/api/version");
            
            // If version.json exists (in production), verify the response structure
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var versionInfo = await response.Content.ReadFromJsonAsync<VersionInfo>();
                
                Assert.NotNull(versionInfo);
                // At minimum, one of these should be populated if version info is available
                Assert.True(
                    !string.IsNullOrEmpty(versionInfo.CommitSha) ||
                    !string.IsNullOrEmpty(versionInfo.BuildTime) ||
                    !string.IsNullOrEmpty(versionInfo.BranchName),
                    "Version info should contain at least one populated field"
                );
            }
        }
    }
}
