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

        [Fact]
        public async Task Get_Weekly_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/Activity/Weekly/2022/01");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_Weekly_WithDifferentWeek_ReturnsSuccess()
        {
            var response = await _httpClientFixture.Client.GetAsync("/Activity/Weekly/2022/02");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_Weekly_WithInvalidYear_Returns404OrError()
        {
            var response = await _httpClientFixture.Client.GetAsync("/Activity/Weekly/invalid/01");
            
            // Should either return 404, 400, or 500 depending on error handling
            Assert.True(
                response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                response.StatusCode == System.Net.HttpStatusCode.InternalServerError,
                $"Expected 404, 400, or 500 but got {response.StatusCode}"
            );
        }
    }
}
