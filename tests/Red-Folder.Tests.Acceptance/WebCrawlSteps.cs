using Red_Folder.Tests.Acceptance.Helpers;
using TechTalk.SpecFlow;
using Xunit;

namespace Red_Folder.Tests.Acceptance
{
    [Binding]
    public class WebCrawlSteps
    {
        private CrawlWrapper _crawler = null;

        [Given(@"I have a web crawler")]
        public void GivenIHaveAWebCrawler()
        {
            _crawler = new CrawlWrapper();
        }
        
        [Given(@"I populate it with (.*)")]
        public void GivenIPopulateItWith(string url)
        {
            //_crawler.AddUrl(url);
        }
        
        [When(@"I run it")]
        public void WhenIRunIt()
        {
            _crawler.Crawl();
        }
        
        [Then(@"there should be no invalid urls")]
        public void ThenThereShouldBeNoInvalidUrls()
        {
            Assert.True(_crawler.Valid(), _crawler.ToString());
        }
    }
}
