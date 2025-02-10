using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace RedFolder.Blog
{
    public class BlogClient
    {
        private readonly HttpClient _httpClient;
        private string _blogUrl;

        // TODO - pass in the IHttpClientFactory
        public BlogClient(HttpClient httpClient, string blogUrl)
        {
            //_httpClient = httpClientFactory.CreateClient("blog");
            _httpClient = httpClient;
            _blogUrl = blogUrl;
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
