using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    [UsesVerify]
    public class SiteMapTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public SiteMapTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task Get_SiteMap_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/sitemap.xml");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.XmlFormatter.FormatXml (raw);

            var settings = new VerifySettings();
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }
    }
}
