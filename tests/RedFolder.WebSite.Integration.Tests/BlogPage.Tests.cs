using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [Collection("HttpClient collection")]
    [UsesVerify]
    public class BlogPageTests
    {
        private readonly HttpClientFixture _httpClientFixture;

        public BlogPageTests(HttpClientFixture httpClientFixture)
        {
            _httpClientFixture = httpClientFixture;
        }

        [Fact]
        public async Task Get_BlogList_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/blog");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_BlogList_WithPagination_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/blog?pageNo=1&blogsPerPage=5");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_BlogList_WithNewsletterFilter_ReturnsSuccess()
        {
            var response = await _httpClientFixture.Client.GetAsync("/blog?filterBy=Newsletter");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_NewsletterArchive_ReturnsSuccess()
        {
            var response = await _httpClientFixture.Client.GetAsync("/NewsletterArchive");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_BlogPost_ReturnsSuccessAndCorrectContent()
        {
            var response = await _httpClientFixture.Client.GetAsync("/blog/developer-laziness-leads-to-productivity");
            response.EnsureSuccessStatusCode();

            var raw = await response.Content.ReadAsStringAsync();
            var formatted = Utils.HtmlFromatter.FormatHtml(raw);

            var settings = new VerifySettings();
            settings.ScrubLinesContaining("<a id=\"cookie-consent-acceptance\"");
            await Verifier.Verify(formatted, settings).UseDirectory("Snapshots");
        }

        [Fact]
        public async Task Get_BlogPost_WithInvalidUrl_RedirectsTo404()
        {
            var response = await _httpClientFixture.Client.GetAsync("/blog/this-blog-post-does-not-exist");
            
            Assert.Equal(System.Net.HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/errors/status/404", response.Headers.Location.ToString());
        }
    }
}
