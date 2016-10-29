using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedFolder.Blog.Markdown.Tests.Unit
{
    public class MarkdownTransformerTests
    {
        [Fact]
        public void Blog_From_Valid_Meta_And_Markdown()
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

            Assert.Contains("<h2>Hello World</h2>", result.Text);
            Assert.Contains("<p>Text</p>", result.Text);
        }
    }
}
