using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    [UsesVerify]
    public class ErrorsPageTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public ErrorsPageTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task Get_Error404Page_ReturnsCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/errors/status/404");
            response.EnsureSuccessStatusCode();
            
            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_Error500Page_ReturnsCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/errors/status/500");
            response.EnsureSuccessStatusCode();
            
            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_RecentProjectsOldUrl_RedirectsToProjects()
        {
            // This tests the special redirect logic in ErrorsController
            var response = await _httpClientFixture.Client.GetAsync("/home/recentprojects");
            
            Assert.Equal(System.Net.HttpStatusCode.MovedPermanently, response.StatusCode);
            Assert.Contains("/projects", response.Headers.Location.ToString().ToLower());
        }

        [Fact]
        public async Task Get_NonExistentPage_Returns404()
        {
            var response = await _httpClientFixture.Client.GetAsync("/this-page-does-not-exist");
            
            // Should get a 404 which is then re-executed to /errors/status/404
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
