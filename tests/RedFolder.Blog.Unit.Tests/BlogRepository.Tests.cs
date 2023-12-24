using Xunit;

namespace RedFolder.Blog.Unit.Tests
{
    public class BlogRepositoryTests
    {
        [Fact]
        public void CanInstanciate()
        {
            var sut = new BlogRepository("https://example.com");
            Assert.NotNull(sut);
        }
    }
}
