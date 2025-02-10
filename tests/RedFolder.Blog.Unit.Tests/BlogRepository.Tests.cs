using RedFolder.Blog.Models;
using System.Net.Http;
using Xunit;

namespace RedFolder.Blog.Unit.Tests
{
    public class BlogRepositoryTests
    {
        [Fact]
        public void CanInstanciate()
        {
            var sut = new BlogRepository(new HttpClient(), new BlogConfiguration { BlogUrl = "https://example.com" });
            Assert.NotNull(sut);
        }
    }
}
