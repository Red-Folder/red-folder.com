using Microsoft.AspNetCore.Hosting;
using System;
using Red_Folder.Website.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RedFolder.Services
{
    public class BlogRepository : IBlogRepository
    {
        //private IHostingEnvironment _hostingEnvironment;
        private string _folder;

        public BlogRepository(IHostingEnvironment hostingEnvironment)
        {
            _folder = hostingEnvironment.WebRootPath + @"\tmp\";
            //_hostingEnvironment = hostingEnvironment;
        }

        public IList<Blog> GetAll()
        {
            var blogs = new List<Blog>();

            foreach (var filename in Directory.GetFiles(_folder, "*.json"))
            {
                // Convert back to a Url
                var url = filename.Replace(_folder, "").Replace(".json", "");
                try
                {
                    var blog = GetBlog(url);
                    if (blog.Enabled)
                    {
                        blogs.Add(blog);
                    }
                }
                catch (BlogNotValidException ex)
                {

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return blogs;
        }

        public Blog Get(string url)
        {
            var blog = GetBlog(url);

            if (blog.Enabled)
            {
                return blog;
            }
            else
            {
                throw new BlogNotEnabledException();
            }
        }

        private Blog GetBlog(string url)
        {
            string htmlFilename = _folder + url + ".html";
            string metaFilename = _folder + url + ".json";

            if (BlogExists(htmlFilename, metaFilename))
            {
                return LoadBlog(htmlFilename, metaFilename);
            }

            throw new BlogNotFoundException();
        }

        private bool BlogExists(string htmlFilename, string metaFilename)
        {
            return File.Exists(htmlFilename) && File.Exists(metaFilename);
        }

        private Blog LoadBlog(string htmlFilename, string metaFilename)
        {
            try
            {
                var htmlContents = File.ReadAllText(htmlFilename);
                JObject meta = JObject.Parse(File.ReadAllText(metaFilename));

                var blog = new Blog
                {
                    Url = (string)meta["url"],
                    Author = "Mark Taylor",
                    Published = ConvertJsonToDateTime((string)meta["published"]),
                    Modified = ConvertJsonToDateTime((string)meta["modified"]),
                    Title = (string)meta["title"],
                    Text = htmlContents,
                    Enabled = Boolean.Parse((string)meta["enabled"]),

                    Description = "RFC Weekly - a summary of things that I find interesting.  It is an indulgence; its the weekly update that I would like to receive.  Unfortunately no-one else is producing it so I figured I best get on with it.  Hopefully someone else will also find useful.",
                    Image = @"https://www.red-folder.com/media/blog/rfc-weekly/RFCWeeklyTwitterCard.png",
                    TwitterHandle = "@folderred"
                };

                return blog;
            }
            catch (Exception ex)
            {
                throw new BlogNotValidException();
            }
        }

        private DateTime ConvertJsonToDateTime(string value)
        {
            if (value.Split('-').Length == 3)
            {
                var valueArray = value.Split('-');
                var year = int.Parse(valueArray[0]);
                var month = int.Parse(valueArray[1]);
                var day = int.Parse(valueArray[2]);

                return new DateTime(year, month, day);
            }
            else
            {
                return DateTime.Now;
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
