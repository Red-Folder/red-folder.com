using Newtonsoft.Json.Linq;
using Xunit;
using RedFolder.Blog.Markdown.Transformers;

namespace Red_Folder.Blog.Markdown.Tests.Unit.Transformers
{
    public class CoreTransformerTests
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
            var markdown = "Hello World\r\n-----------\r\nText\r\n";

            var uat = new CoreTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.Contains("<h2>Hello World</h2>", result);
            Assert.Contains("<p>Text</p>", result);
        }
    }
}
