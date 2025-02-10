using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Moq;
using Moq.Protected;
using System;
using System.IO;

namespace RedFolder.WebSite.Integration.Tests.Infrastructure
{
    public class MockHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _default = new HttpClient();

        public HttpClient CreateClient(string name)
        {
            switch (name.ToLower())
            {
                case "podcast": return CreatePodcastClient();
            }

            // TODO - should throw an error if not set
            return _default;
        }

        public HttpClient CreatePodcastClient()
        {
            var podcastDetails = File.ReadAllText("Data/150.md");
            var content = new StringContent(podcastDetails);

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString().Equals("https://content.red-folder.com/podcasts/150-An-updated-pitch-for-the-podcast.md", StringComparison.OrdinalIgnoreCase)),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = content
               })
               .Verifiable();

            return new HttpClient(handlerMock.Object);
        }
    }
}
