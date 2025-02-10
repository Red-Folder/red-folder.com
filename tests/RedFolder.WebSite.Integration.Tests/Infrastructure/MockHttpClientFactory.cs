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
        public HttpClient CreateClient(string name)
        {
            switch (name.ToLower())
            {
                case "podcast": return CreatePodcastClient();
                case "activity": return CreateActivityClient();
                case "blog": return CreateBlogClient();
                case "recaptcha": return CreateReCaptchaClient();
            }

            throw new NotSupportedException($"HttpClient mock implementation not available for ${name}");
        }

        private HttpClient CreatePodcastClient()
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

        private HttpClient CreateActivityClient()
        {
            var json = File.ReadAllText("Data/activity-weekly-2022-01.json");
            var content = new StringContent(json);

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString().StartsWith("https://rfc-activity.azurewebsites.net/api/weeklyactivity/2022/01?code=", StringComparison.OrdinalIgnoreCase)),
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

        private HttpClient CreateBlogClient()
        {
            var blogFeedJson = File.ReadAllText("Data/blog-feed.json");
            var blogFeedContent = new StringContent(blogFeedJson);

            var blogHtml = File.ReadAllText("Data/developer-laziness-leads-to-productivity.html");
            var blogContent = new StringContent(blogHtml);

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync((HttpRequestMessage req, CancellationToken token) =>
               {
                   if (req.RequestUri.ToString().Contains("https://rfc-docs-production.azurewebsites.net/api/Blog?code="))
                   {
                       return new HttpResponseMessage()
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = blogFeedContent
                       };
                   }
                   else if (req.RequestUri.ToString().Equals("https://rfcdocsproduction.blob.core.windows.net/blog/developer-laziness-leads-to-productivity/developer-laziness-leads-to-productivity.html"))
                   {
                       return new HttpResponseMessage()
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = blogContent
                       };
                   }
                   return new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.NotFound
                   };
               })
               .Verifiable();

            return new HttpClient(handlerMock.Object);
        }

        private HttpClient CreateReCaptchaClient()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            return new HttpClient(handlerMock.Object);
        }
    }
}
