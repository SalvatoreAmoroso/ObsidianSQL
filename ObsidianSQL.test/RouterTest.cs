using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObsidianSQL.server;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.src.http;
using ObsidianSQL.test.FakeObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ObsidianSQL.test
{
    [TestClass]
    public class RouterTest
    {
        [TestMethod]
        public void FindCorrectRouteTest()
        {
            Func<IRequest, IResponse> handler = (request) => {
                return new Response("test-response", 200);
            };

            Router router = new();
            router.RegisterRoute(new Route(new string[] { "test" }, handler));

            Request request = new()
            {
                Url = new Uri("http://localhost:8080/test")
            };

            IResponse routerResponse = router.ManageRequest(request);

            Assert.AreEqual(200, routerResponse.HttpStatusCode);
            Assert.AreEqual("test-response", routerResponse.Content);
        }

        [TestMethod]
        public void FindPlaceHoldersInUrlTest()
        {
            Func<IRequest, IResponse> handler = (request) => {
                Assert.AreEqual("placeholderValue", request.UrlPlaceholderValues[0]);
                return new Response("test-response", 200);
            };

            Router router = new();
            router.RegisterRoute(new Route(new string[] { "test", "*" }, handler));

            Request request = new()
            {
                Url = new Uri("http://localhost:8080/test/placeholderValue")
            };

            IResponse routerResponse = router.ManageRequest(request);
        }
    }
}
