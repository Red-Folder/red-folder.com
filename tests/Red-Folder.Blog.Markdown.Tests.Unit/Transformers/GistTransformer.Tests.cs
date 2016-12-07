﻿
using Newtonsoft.Json.Linq;
using RedFolder.Blog.Markdown.Transformers;
using Xunit;

namespace Red_Folder.Blog.Markdown.Tests.Unit.Transformers
{
    public class GistTransformerTests
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
            var markdown = "<p>Line1\n%[https://gist.github.com/3778380.js]\nLine4</p>";
            var expected = "<p>Line1\n<script src=\"https://gist.github.com/3778380.js\"></script>\nLine4</p>";

            var uat = new GistTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.Equal(expected, result);
        }
    }
}
