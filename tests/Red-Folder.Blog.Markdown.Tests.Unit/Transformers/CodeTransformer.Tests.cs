using Newtonsoft.Json.Linq;
using Xunit;
using RedFolder.Blog.Markdown.Transformers;

namespace Red_Folder.Blog.Markdown.Tests.Unit.Transformers
{
    public class CodeTrasnformerTests
    {
        [Fact]
        public void Correctly_Convert_Code_Tag()
        {
            var meta = JObject.Parse(
            @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""enabled"":  ""true""
                        }");
            var markdown = "<p>Line1\n<code>Line2\nLine3\n</code>\nLine4</p>";
            var expected = "<p>Line1\n<pre><code>Line2\nLine3\n</code></pre>\nLine4</p>";

            var uat = new CodeTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Correctly_Convert_Tripple_Tick()
        {
            var meta = JObject.Parse(
            @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""enabled"":  ""true""
                        }");
            var markdown = "<p>Line1\n```\nLine2\nLine3\n```\nLine4</p>";
            var expected = "<p>Line1\n<pre><code>Line2\nLine3\n</code></pre>\nLine4</p>";

            var uat = new CodeTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Doesnt_Interpret_Comments_In_Code_As_Markdown()
        {
            var meta = JObject.Parse(
            @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""enabled"":  ""true""
                        }");
            var markdown = "Line1\n# Line2\n```\n# Line3\n```\nLine4\n";
            var expected = "<p>Line1</p>\n\n<h1>Line2</h1>\n\n<pre><code># Line3\n</code></pre>\n\n<p>Line4</p>";

            // Needs to have the Core Transformer to validate the test
            var uat = new CodeTransformer(new CoreTransformer());

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.Equal(expected, result);
        }

    }
}
