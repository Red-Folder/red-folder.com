using Red_Folder.Tests.Acceptance.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace Red_Folder.Tests.Acceptance
{
    [Binding]
    public sealed class RedFolderSteps
    {
        private ClientWrapper _clientWrapper;

        [Given(@"I access (.*)")]
        public void GivenIAccessTheHomepage(string url)
        {
            _clientWrapper = new ClientWrapper(url);
            _clientWrapper.Get();
        }

        [Then(@"I should receive (.*) response")]
        public void ThenIShouldReceiveResponse(string responseChooser)
        {
            HttpStatusCode expectedCode = HttpStatusCode.OK;

            switch (responseChooser.ToLower())
            {
                case "ok":
                    expectedCode = HttpStatusCode.OK;
                    break;
                case "not found":
                    expectedCode = HttpStatusCode.NotFound;
                    break;
                case "error":
                    expectedCode = HttpStatusCode.InternalServerError;
                    break;
                case "permanent redirect":
                    expectedCode = HttpStatusCode.MovedPermanently;
                    break;
                default:
                    throw new ArgumentException(String.Format("Not a valid response choice: '{0}'", responseChooser));
            }

            Assert.Equal(expectedCode, _clientWrapper.LastHttpStatusCode);
        }

        [Then(@"location of (.*)")]
        public void ThenLocationOf(string expectedLocation)
        {
            var location = _clientWrapper.LastHttpResponseHeaders.Where(x => x.Key.ToLower() == "location").FirstOrDefault();
            Assert.NotNull(location);

            var actualLocation = location.Value.FirstOrDefault();
            Assert.Equal(expectedLocation, actualLocation);
        }

        [Then(@"the cannonical is /projects/cordova")]
        public void ThenTheCannonicalIsProjectsCordova()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
