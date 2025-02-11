using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    [UsesVerify]
    public class HomePageTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public HomePageTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task Get_HomePage_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            
            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<input name=\"__RequestVerificationToken\"");
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_ContactPage_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/home/contact");
            response.EnsureSuccessStatusCode();
            
            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<input name=\"__RequestVerificationToken\"");
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_CookiePolicyPage_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/home/cookiepolicy");
            response.EnsureSuccessStatusCode();
            
            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<input name=\"__RequestVerificationToken\"");
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_Redirect_RedirectsForKnownRoutes()
        {
            var response = await _httpClientFixture.Client.GetAsync("/redirect?url=http://blog.red-folder.com/2016/09/rfc-weekly-12th-september-2016.html");

            Assert.Equal(System.Net.HttpStatusCode.MovedPermanently, response.StatusCode);
            Assert.Contains("/blog/rfc-weekly-12th-september-2016", response.Headers.Location.ToString());
        }

        [Fact]
        public async Task Get_Redirect_RedirectsToNotFoundForUnknownRoutes()
        {
            var response = await _httpClientFixture.Client.GetAsync("/redirect?url=something-random");

            Assert.Equal(System.Net.HttpStatusCode.Found, response.StatusCode);
            Assert.Contains("/errors/status/404", response.Headers.Location.ToString());
        }
    }
}
