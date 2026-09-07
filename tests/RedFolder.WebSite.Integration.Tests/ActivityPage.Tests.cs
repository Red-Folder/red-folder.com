using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace RedFolder.WebSite.Integration.Tests;

[Collection("HttpClient collection")]
public class ActivityPageTests
{
    private readonly HttpClientFixture _fixture;
    public ActivityPageTests(HttpClientFixture fixture) => _fixture = fixture;

    [Theory]
    [InlineData("/Activity")]
    [InlineData("/Activity/Weekly/2022/01")]
    [InlineData("/Activity/Books/2022")]
    [InlineData("/Activity/Skills/2022")]
    [InlineData("/Activity/unknown")]
    public async Task Get_RetiredRoute_ReturnsGone(string path)
    {
        var response = await _fixture.Client.GetAsync(path);

        Assert.Equal(HttpStatusCode.Gone, response.StatusCode);
    }
}
