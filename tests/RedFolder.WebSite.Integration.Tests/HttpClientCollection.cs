using Xunit;

namespace RedFolder.WebSite.Integration.Tests
{
    [CollectionDefinition("HttpClient collection")]
    public class HttpClientCollection : ICollectionFixture<HttpClientFixture>
    {
    }
}
