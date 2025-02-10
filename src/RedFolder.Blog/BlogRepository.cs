using RedFolder.Blog.Models;
using RedFolder.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RedFolder.Blog
{
    public class BlogRepository : IBlogRepository, IRedirectRepository, ISiteMapUrlRepository
    {
        private BlogClient _client;

        private IList<Website.Data.Blog> _blogs;

        // TODO - pass in the IHttpClientFactory
        public BlogRepository(HttpClient httpClient, BlogConfiguration configuration)
        {
            _client = new BlogClient(httpClient, configuration.BlogUrl);
        }

        private IList<RedFolder.Website.Data.Blog> Blogs
        {
            get
            {
                if (_blogs == null)
                {
                    _blogs = _client.GetAll();
                }

                return _blogs;
            }
        }

        public IList<RedFolder.Website.Data.Blog> GetAll()
        {
            return Blogs.Where(b => b.Enabled).OrderByDescending(b => b.Published).ToList();
        }

        public RedFolder.Website.Data.Blog Get(string url)
        {
            var blogs = Blogs;
            var matchingBlogs = blogs.Where(b => b.Url.ToLower() == url.ToLower());

            if (matchingBlogs == null || matchingBlogs.Count() != 1)
            {
                throw new BlogNotFoundException();
            }

            var blog = matchingBlogs.First();

            if (blog.Enabled)
            {
                return blog;
            }
            else
            {
                throw new BlogNotEnabledException();
            }
        }

        public string LoadContent(Website.Data.Blog blog)
        {
            return _client.LoadContent(blog);
        }

        public Dictionary<string, List<Redirect>> GetRedirects()
        {
            var query = Blogs
                    .Where(b => b.Enabled)
                    .Where(b => b.Redirects != null && b.Redirects.Count > 0)
                    .Select(b => new KeyValuePair<string, List<Redirect>>(b.Url, b.Redirects));

            var redirects = new Dictionary<string, List<Redirect>>();
            foreach (var redirect in query)
            {
                redirects.Add(redirect.Key, redirect.Value);
            }

            return redirects;
        }

        public List<string> GetUrls()
        {
            return Blogs.Where(b => b.Enabled).OrderByDescending(b => b.Published).Select(b => "/blog/" + b.Url).ToList();
        }

        public void Reload()
        {
            _blogs = null;
        }
    }

    public class BlogNotFoundException : Exception
    {
    }

    public class BlogNotEnabledException : Exception
    {
    }

    public class BlogNotValidException : Exception
    {
    }
}
