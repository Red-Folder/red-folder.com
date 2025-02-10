using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    [UsesVerify]
    public class ActivityPageTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public ActivityPageTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact(Skip = "resolving CI issue")]
        public async Task Get_Weekly_eturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/Activity/Weekly/2022/01");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }
    }
}
