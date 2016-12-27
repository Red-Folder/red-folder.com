﻿using Microsoft.AspNetCore.Hosting;
using System;
using RedFolder.Website.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using RedFolder.Blog.Markdown;
using System.Linq;
using RedFolder.ViewModels;

namespace RedFolder.Services
{
    public class BlogRepository : IBlogRepository, IRedirectRepository, ISiteMapUrlRepository
    {
        //private IHostingEnvironment _hostingEnvironment;
        private string _folder;
        private MarkdownRepository _markdownRepository;

        private IList<RedFolder.Website.Data.Blog> _blogs;

        public BlogRepository(IHostingEnvironment hostingEnvironment)
        {
            _folder = hostingEnvironment.WebRootPath + @"\tmp\";
            //_hostingEnvironment = hostingEnvironment;

            _markdownRepository = new MarkdownRepository();
        }

        private IList<RedFolder.Website.Data.Blog> Blogs
        {
            get
            {
                if (_blogs == null)
                {
                    _blogs = _markdownRepository.Import(_folder);
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

        public Dictionary<string, List<Redirect>> GetRedirects()
        {
            var query = Blogs
                    .Where(b => b.Enabled)
                    .Where(b => b.Redirects != null && b.Redirects.Count > 0)
                    .Select(b => new KeyValuePair<string, List<Redirect>>(b.Url, b.Redirects ));

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
