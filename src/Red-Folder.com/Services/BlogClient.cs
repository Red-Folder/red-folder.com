using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public class BlogClient
    {
        private static HttpClient _httpClient = new HttpClient();
        private string _blogUrl;

        public BlogClient(string blogUrl)
        {
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
