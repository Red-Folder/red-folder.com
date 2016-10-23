using RedFolder.Blog.Markdown.Tests.Unit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedFolder.Blog.Markdown.Tests.Unit
{
    public class BlogLoaderTests
    {
        [Fact]
        public void Correctly_Gets_Blog_From_Folder()
        {
            var mockFileSystem = new FileSystemBuilder()
                                    .AddFileContents(@"c:\valid\folder\blog1",
                                    @"blog1.json",
                                    @"{
                                        ""url"": ""/blog1""
                                    }")
                                    .AddFileContents(@"c:\valid\folder\blog1",
                                    @"blog1.md",
                                    @"
                                        Blog1
                                        =====
                                        Text
                                    ")
                                    .Build();
            var mockMarkdownTransformer = new MarkdownTransformerBuilder()
                                    .AddBlog(new Website.Data.Blog())
                                    .Build();

            var uat = new BlogLoader(mockFileSystem, mockMarkdownTransformer);

            var result = uat.GetBlog(@"c:\valid\folder\blog1");

            Assert.NotNull(result);
        }
    }
}
