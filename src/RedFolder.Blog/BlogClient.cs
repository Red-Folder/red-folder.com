using Newtonsoft.Json;
using RedFolder.Blog.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace RedFolder.Blog
{
    public class BlogClient : IBlogClient
    {
        private readonly HttpClient _httpClient;
        private string _blogUrl;

        public BlogClient(IHttpClientFactory httpClientFactory, BlogConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("blog");
            _blogUrl = configuration.BlogUrl;
        }

        public IList<RedFolder.Website.Data.Blog> GetAll()
        {
            var rawResponse = _httpClient.GetStringAsync(_blogUrl).Result;

            var blogs = JsonConvert.DeserializeObject<IList<RedFolder.Website.Data.Blog>>(rawResponse);

            return blogs;
        }

        public string LoadContent(RedFolder.Website.Data.Blog blog)
        {
            return _httpClient.GetStringAsync(blog.ContentUrl).Result;
        }
    }
}
