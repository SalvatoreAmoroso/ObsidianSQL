using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObsidianSQL.server;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.src.http;
using ObsidianSQL.test.FakeObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ObsidianSQL.test
{
    [TestClass]
    public class RequestListenerTest
    {
        [TestMethod]
        public void RequestIsPassedToRouterTest()
        {
            FakeHttpListener fakeListener = new();
            Request request = new()
            {
                Url = new System.Uri("http://localhost:8080/test"),
                HttpMethod = "GET",
                AuthToken = "auth"
            };
            fakeListener.SetRequest(request);

            FakeRouter fakeRouter = new();
            fakeRouter.Response = new Response("", 200);

            RequestListener listener = new(fakeListener, new string[] { }, fakeRouter);
            Task.Run(() => listener.HandleRequests());
            Thread.Sleep(100);
            listener.Stop();

            IRequest requestFromRouter = fakeRouter.Request;

            Assert.IsNotNull(requestFromRouter);
            Assert.AreEqual(request, requestFromRouter);
            Assert.AreEqual(request.Url, requestFromRouter.Url);
            Assert.AreEqual("GET", requestFromRouter.HttpMethod);
            Assert.AreEqual("auth", requestFromRouter.AuthToken);
        }

        [TestMethod]
        public void ExceptionShouldGiveErrorCodeTest()
        {
            FakeHttpListener fakeListener = new();
            Request request = new()
            {
                Url = new System.Uri("http://localhost:8080/test"),
                HttpMethod = "GET",
                AuthToken = "auth"
            };
            fakeListener.SetRequest(request);

            FakeRouter fakeRouter = new();
            fakeRouter.Response = new Response("", 200);
            fakeRouter.ThrowException = true;
            fakeRouter.ExceptionToThrow = new BadRequestException("test");

            RequestListener listener = new(fakeListener, new string[] { }, fakeRouter);
            Task.Run(() => listener.HandleRequests());
            Thread.Sleep(100);
            listener.Stop();

            IResponse responseFromListener = fakeListener.GetResponse();

            Assert.IsNotNull(responseFromListener);
            Assert.AreEqual(400, responseFromListener.HttpStatusCode);
        }

        [TestMethod]
        public void PrefixesPassedToHttpListenerTest()
        {
            FakeHttpListener fakeListener = new();

            RequestListener listener = new(fakeListener, new string[] { "test1", "test2", "test3" }, null);

            Assert.AreEqual("test1", fakeListener.Prefixes[0]);
            Assert.AreEqual("test2", fakeListener.Prefixes[1]);
            Assert.AreEqual("test3", fakeListener.Prefixes[2]);
        }
    }
}
