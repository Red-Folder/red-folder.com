using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown.Tests.Unit.Helpers
{
    public class MarkdownTransformerBuilder
    {
        Mock<IMarkdownTransformer> _mock;

        public MarkdownTransformerBuilder()
        {
            _mock = new Mock<IMarkdownTransformer>();
        }

        public MarkdownTransformerBuilder AddBlog(RedFolder.Website.Data.Blog blog)
        {
            _mock.Setup(m => m.Transform(It.IsAny<JObject>(), It.IsAny<string>())).Returns(blog);
            return this;
        }

        public IMarkdownTransformer Build()
        {
            return _mock.Object;
        }
    }
}
