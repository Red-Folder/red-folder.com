using Moq;
using Newtonsoft.Json.Linq;
using System;
using Xunit;
using RedFolder.Blog.Markdown.Transformers;

namespace RedFolder.Blog.Markdown.Tests.Unit
{
    public class MarkdownTransformerTests
    {
        [Fact]
        public void Blog_From_Valid_Meta_And_Markdown_With_No_Inner()
        {
            var meta = JObject.Parse(
                        @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""enabled"":  ""true""
                        }");
            var markdown = "Hello World\r\n-----------\r\nText\r\n";

            var uat = new MarkdownTransformer();

            var result = uat.Transform(meta, markdown);

            Assert.NotNull(result);

            Assert.Equal(@"/rfc-weekly-17th-October-2016", result.Url);
            Assert.Equal(new DateTime(2016, 10, 17), result.Published);
            Assert.Equal(new DateTime(2016, 10, 17), result.Modified);
            Assert.Equal("RFC Weekly - 17th October 2016", result.Title);
            Assert.Equal(true, result.Enabled);

            Assert.Contains(markdown, result.Text);
        }

        [Fact]
        public void Blog_From_Valid_Meta_And_Markdown_With_Inner()
        {
            var meta = JObject.Parse(
                        @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""enabled"":  ""true""
                        }");
            var markdown = "Hello World\r\n-----------\r\nText\r\n";

            Mock<ITransformer> mock = new Mock<ITransformer>();
            mock.Setup(m => m.TransformMarkdown(It.IsAny<JObject>(), It.IsAny<string>())).Returns("ABCDEF");

            var uat = new MarkdownTransformer(mock.Object);

            var result = uat.Transform(meta, markdown);

            Assert.NotNull(result);

            Assert.Equal(@"/rfc-weekly-17th-October-2016", result.Url);
            Assert.Equal(new DateTime(2016, 10, 17), result.Published);
            Assert.Equal(new DateTime(2016, 10, 17), result.Modified);
            Assert.Equal("RFC Weekly - 17th October 2016", result.Title);
            Assert.Equal(true, result.Enabled);

            Assert.Contains("ABCDEF", result.Text);
        }
    }
}
