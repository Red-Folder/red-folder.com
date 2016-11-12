using Xunit;
using Moq;
using Newtonsoft.Json.Linq;
using RedFolder.Blog.Markdown.Transformers;

namespace Red_Folder.Blog.Markdown.Tests.Unit.Transformers
{
    public class BaseTransformerTests
    {
        [Fact]
        public void No_Change_With_No_Inner()
        {
            var uat = new BaseTransformer();

            var result = uat.TransformMarkdown(null, "1234567890");

            Assert.Equal("1234567890", result);
        }

        [Fact]
        public void Change_With_Inner_Pre()
        {
            Mock<ITransformer> mock = new Mock<ITransformer>();
            mock.Setup(m => m.TransformMarkdown(It.IsAny<JObject>(), It.IsAny<string>())).Returns("ABCDEF");

            var uat = new BaseTransformer(mock.Object);

            var result = uat.TransformMarkdown(null, "1234567890");

            Assert.Equal("ABCDEF", result);
        }
    }
}
