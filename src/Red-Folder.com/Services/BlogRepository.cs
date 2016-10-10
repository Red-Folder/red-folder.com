using Microsoft.AspNetCore.Hosting;
using System;
using Red_Folder.Website.Data;
using System.IO;

namespace RedFolder.Services
{
    public class BlogRepository: IBlogRepository
    {
        private IHostingEnvironment _hostingEnvironment;

        public BlogRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public Blog Get(string url)
        {
            var blog = new Blog
            {
                Url = "\rfc-weekly-10th-October-2016",
                Author = "Mark Taylor",
                Published = new DateTime(2016, 10, 10),
                Title = "RFC Weekly - 10th October 2016",
                Text = GetFilecontent(),

                Twitter = new Twitter
                {
                    Username = "@folderred",
                    Title = "RFC Weekly - 10th October 2016",
                    Description = "RFC Weekly - a summary of things that I find interesting.  It is an indulgence; its the weekly update that I would like to receive.  Unfortunately no-one else is producing it so I figured I best get on with it.  Hopefully someone else will also find useful.",
                    Image = @"https://www.red-folder.com/media/blog/rfc-weekly/RFCWeeklyTwitterCard.png"
                }
            };

            return blog;
        }

        private string GetFilecontent()
        {
            var filename = _hostingEnvironment.WebRootPath + @"\tmp\rfcweekly.html";

            return File.ReadAllText(filename);
        }
    }
}
