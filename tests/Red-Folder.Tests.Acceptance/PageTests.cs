using Red_Folder.Tests.Acceptance.Helpers;
using System;
using System.Linq;
using System.Net;
using Xunit;

namespace Red_Folder.Tests.Acceptance
{
    public class PageTests
    {
        private readonly string _host;

        public PageTests()
        {
            _host = Environment.GetEnvironmentVariable("Red-Folder_Tests_Acceptance_Host");
        }

        [Fact]
        public void Going_to_the_homepage_is_valid()
        {
            var client = new WebClientBuilder(_host, "/")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_services_page_is_valid()
        {
            var client = new WebClientBuilder(_host, "/services")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_projects_page_is_valid()
        {
            var client = new WebClientBuilder(_host, "/projects")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_roi_page_is_valid()
        {
            var client = new WebClientBuilder(_host, "/projects/roi")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_my_bio_page_is_valid()
        {
            var client = new WebClientBuilder(_host, "/mybio")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_legacy_recent_projects_page_should_return_redirect()
        {
            var client = new WebClientBuilder(_host, "/home/recentprojects")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.MovedPermanently, client.LastHttpStatusCode);
            Assert.Equal("/projects", 
                         client.LastHttpResponseHeaders.Where(x => x.Key.ToLower() == "location").FirstOrDefault().Value.FirstOrDefault(),
                         ignoreCase: false);
        }

        [Fact]
        public void Going_to_the_exception_page_should_return_error()
        {
            var client = new WebClientBuilder(_host, "/home/throw")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.InternalServerError, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_a_non_existent_page_should_return_not_found()
        {
            var client = new WebClientBuilder(_host, "/home/idontexist")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.NotFound, client.LastHttpStatusCode);
        }
    }
}
