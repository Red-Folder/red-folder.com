using Red_Folder.Tests.Acceptance.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Red_Folder.Tests.Acceptance
{
    public class PageTests
    {
        [Fact]
        public void Going_to_the_homepage_is_valid()
        {
            var client = new WebClientBuilder(Config.Host, "/")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_services_page_is_valid()
        {
            var client = new WebClientBuilder(Config.Host, "/services")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_projects_page_is_valid()
        {
            var client = new WebClientBuilder(Config.Host, "/projects")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_roi_page_is_valid()
        {
            var client = new WebClientBuilder(Config.Host, "/projects/roi")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_my_bio_page_is_valid()
        {
            var client = new WebClientBuilder(Config.Host, "/mybio")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.OK, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_the_legacy_recent_projects_page_should_return_redirect()
        {
            var client = new WebClientBuilder(Config.Host, "/home/recentprojects")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.MovedPermanently, client.LastHttpStatusCode);
            Assert.Equal("/projects", client.LastHttpResponseHeaders.Where(x => x.Key.ToLower() == "location").FirstOrDefault().Value.FirstOrDefault());
        }

        [Fact]
        public void Going_to_the_exception_page_should_return_error()
        {
            var client = new WebClientBuilder(Config.Host, "/home/throw")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.InternalServerError, client.LastHttpStatusCode);
        }

        [Fact]
        public void Going_to_a_non_existent_page_should_return_not_found()
        {
            var client = new WebClientBuilder(Config.Host, "/home/idontexist")
                            .Build();

            client.Get();

            Assert.Equal(HttpStatusCode.NotFound, client.LastHttpStatusCode);
        }


    }
}
