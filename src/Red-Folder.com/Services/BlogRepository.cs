using Microsoft.AspNetCore.Hosting;
using System;
using RedFolder.Website.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using RedFolder.Blog.Markdown;
using System.Linq;

namespace RedFolder.Services
{
    public class BlogRepository : IBlogRepository
    {
        //private IHostingEnvironment _hostingEnvironment;
        private string _folder;

        private MarkdownRepository _markdownRepository;

        public BlogRepository(IHostingEnvironment hostingEnvironment)
        {
            _folder = hostingEnvironment.WebRootPath + @"\tmp\";
            //_hostingEnvironment = hostingEnvironment;

            _markdownRepository = new MarkdownRepository();
        }

        public IList<RedFolder.Website.Data.Blog> GetAll()
        {
            return _markdownRepository.Import(_folder).Where(b => b.Enabled).OrderByDescending(b => b.Published).ToList();
        }

        public RedFolder.Website.Data.Blog Get(string url)
        {
            var blogs = _markdownRepository.Import(_folder);
            var matchingBlogs = blogs.Where(b => b.Url == url);

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
