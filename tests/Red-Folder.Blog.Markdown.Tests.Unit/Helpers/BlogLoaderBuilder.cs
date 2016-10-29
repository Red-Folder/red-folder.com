using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown.Tests.Unit.Helpers
{
    public class BlogLoaderBuilder
    {
        Mock<IBlogLoader> _mock;

        public BlogLoaderBuilder()
        {
            _mock = new Mock<IBlogLoader>();
        }

        public BlogLoaderBuilder AddBlog(string folder, RedFolder.Website.Data.Blog blog)
        {
            _mock.Setup(m => m.GetBlog(folder)).Returns(blog);
            return this;
        }

        public IBlogLoader Build()
        {
            return _mock.Object;
        }
    }
}
