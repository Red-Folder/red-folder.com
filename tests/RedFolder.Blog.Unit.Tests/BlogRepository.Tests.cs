using Moq;
using Xunit;

namespace RedFolder.Blog.Unit.Tests
{
    public class BlogRepositoryTests
    {
        [Fact]
        public void CanInstanciate()
        {
            var mockBlogClient = new Mock<IBlogClient>();
            var sut = new BlogRepository(mockBlogClient.Object);
            Assert.NotNull(sut);
        }
    }
}
