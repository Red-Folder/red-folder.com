using Newtonsoft.Json.Linq;
using Xunit;
using RedFolder.Blog.Markdown.Transformers;

namespace Red_Folder.Blog.Markdown.Tests.Unit.Transformers
{
    public class CodeTrasnformerTests
    {
        [Fact]
        public void Correctly_Convert_Markdown()
        {
            var meta = JObject.Parse(
            @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""enabled"":  ""true""
                        }");
            var markdown = "<p>Line1\r\n<code>Line2\r\nLine3\r\n</code>\r\nLine4</p>";
            var expected = "<p>Line1\r\n<code>Line2<br/>\r\nLine3<br/>\r\n</code>\r\nLine4</p>";

            var uat = new CodeTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.Equal(expected, result);
        }
    }
}
