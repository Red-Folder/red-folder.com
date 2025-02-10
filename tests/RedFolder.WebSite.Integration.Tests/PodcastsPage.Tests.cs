using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    [UsesVerify]
    public class PodcastsPageTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public PodcastsPageTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact(Skip = "resolving CI issue")]
        public async Task Get_Episode150_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/podcasts/150");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }
    }
}
