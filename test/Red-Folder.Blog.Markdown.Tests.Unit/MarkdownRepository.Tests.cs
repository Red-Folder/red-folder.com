using RedFolder.Blog.Markdown;
using RedFolder.Blog.Markdown.Tests.Unit.Helpers;
using System;
using System.Collections.Generic;
using Xunit;


namespace RedFolder.Blog.Markdown.Tests.Unit
{
    public class MarkdownRepositoryTests
    {
        [Fact]
        public void Import_Works_Correctly()
        {
            var mockBlogLoader = new BlogLoaderBuilder()
                                    .AddBlog(@"c:\valid\folder\blog1", new Website.Data.Blog())
                                    .AddBlog(@"c:\valid\folder\blog2", new Website.Data.Blog())
                                    .AddBlog(@"c:\valid\folder\blog3", new Website.Data.Blog())
                                    .Build();
            var mockFileSystem = new FileSystemBuilder()
                                    .AddFolderStructure(@"c:\valid\folder", new List<string>
                                    {
                                        @"c:\valid\folder\blog1",
                                        @"c:\valid\folder\blog2",
                                        @"c:\valid\folder\blog3"
                                    })
                                    .Build();
            var uat = new MarkdownRepository(mockFileSystem, mockBlogLoader);

            var blogs = uat.Import(@"c:\valid\folder");

            Assert.Equal(3, blogs.Count);
        }
        
        [Fact]
        public void Error_On_Invalid_Path()
        {
            var mockFileSystem = new FileSystemBuilder()
                                    .AddInvalidFolder(@"c:\invalid\folder")
                                    .Build();
            var uat = new MarkdownRepository(mockFileSystem, null);

            Exception ex = Assert.Throws<InvalidOperationException>(() => uat.Import(@"c:\invalid\folder"));

            Assert.Equal("Invalid folder", ex.Message);
        }
    }
}
