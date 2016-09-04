using Moq;
using Red_Folder.WebCrawl.Helpers;
using Xunit;

namespace Red_Folder.WebCrawl.Tests
{
    public class LinksExtractor_ShouldExtractLinks
    {
        [Fact]
        public void ReturnsLinkForSrcWithDoubleQuote()
        {
            Mock<IClientWrapper> clientMock = new Mock<IClientWrapper>();
            clientMock.Setup(x => x.GetLastResponse()).Returns(@"<script src=""/test.js""></script>");

            var extractor = new LinksExtractor(clientMock.Object);

            var results = extractor.Extract();

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsLinkForSrcWithSingleQuote()
        {
            Mock<IClientWrapper> clientMock = new Mock<IClientWrapper>();
            clientMock.Setup(x => x.GetLastResponse()).Returns(@"<script src='/test.js'></script>");

            var extractor = new LinksExtractor(clientMock.Object);

            var results = extractor.Extract();

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsLinkForSrcWithDoubleQuoteAndAdditonalSpaces()
        {
            Mock<IClientWrapper> clientMock = new Mock<IClientWrapper>();
            clientMock.Setup(x => x.GetLastResponse()).Returns(@"<script src = ""/test.js""></script>");

            var extractor = new LinksExtractor(clientMock.Object);

            var results = extractor.Extract();

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsLinkForSitemapLoc()
        {
            Mock<IClientWrapper> clientMock = new Mock<IClientWrapper>();
            clientMock.Setup(x => x.GetLastResponse()).Returns(@"<loc>https://www.red-folder.com/test.js</loc>");

            var extractor = new LinksExtractor(clientMock.Object);

            var results = extractor.Extract();

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsMultipleLinks()
        {
            var content = @"<loc>https://www.red-folder.com/test.js</loc>" +
                          @"<script src = ""/test.js""></script>" +
                          @"<script src='/test.js'></script>" +
                          @"<script src=""/test.js""></script>";


            Mock<IClientWrapper> clientMock = new Mock<IClientWrapper>();
            clientMock.Setup(x => x.GetLastResponse()).Returns(content);

            var extractor = new LinksExtractor(clientMock.Object);

            var results = extractor.Extract();

            Assert.NotNull(results);
            Assert.Equal(4, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
            Assert.Equal("https://www.red-folder.com/test.js", results[1]);
            Assert.Equal("https://www.red-folder.com/test.js", results[2]);
            Assert.Equal("https://www.red-folder.com/test.js", results[3]);
        }

    }
}
