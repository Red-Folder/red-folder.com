using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    [UsesVerify]
    public class ProjectsPageTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public ProjectsPageTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact(Skip = "resolving CI issue")]
        public async Task Get_ProjectsPage_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/projects");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            settings.ScrubLinesContaining("<canvas id=");
            settings.ScrubLinesContaining("window.graphs[");
            settings.ScrubLinesContaining("elementId: '");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }
    }
}
