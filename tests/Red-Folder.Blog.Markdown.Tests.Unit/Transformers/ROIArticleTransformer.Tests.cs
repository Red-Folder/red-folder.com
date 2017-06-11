using Newtonsoft.Json.Linq;
using RedFolder.Blog.Markdown.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Red_Folder.Blog.Markdown.Tests.Unit.Transformers
{
    public class ROIArticleTransformerTests
    {
        [Fact]
        public void MetaContainsNoKeywordThenNoROIBlock()
        {
            var meta = JObject.Parse(
            @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""enabled"":  ""true""
                        }");
            var markdown = "<h2>Hello World</h2>";

            var uat = new ROIArticleTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.DoesNotContain("roi-block", result);
            Assert.Equal(markdown, result);
        }

        [Fact]
        public void MetaContainsNoROIKeywordThenNoROIBlock()
        {
            var meta = JObject.Parse(
            @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""keyWords"":[""IT Management"",""Agile"",""Technical Debt""],
                            ""enabled"":  ""true""
                        }");
            var markdown = "<h2>Hello World</h2>";

            var uat = new ROIArticleTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.DoesNotContain("roi-block", result);
            Assert.Equal(markdown, result);
        }

        [Fact]
        public void MetaContainsROIKeywordThenROIBlock()
        {
            var meta = JObject.Parse(
            @"{
                            ""url"": ""/rfc-weekly-17th-October-2016"",
                            ""published"": ""2016-10-17"",
                            ""modified"": ""2016-10-17"",
                            ""title"": ""RFC Weekly - 17th October 2016"",
                            ""keyWords"":[""ROI"",""IT Management"",""Agile"",""Technical Debt""],
                            ""enabled"":  ""true""
                        }");
            var markdown = "<h2>Hello World</h2>";

            var uat = new ROIArticleTransformer();

            var result = uat.TransformMarkdown(meta, markdown);

            Assert.Contains("roi-block", result);
            Assert.Contains(markdown, result);
        }
    }
}
