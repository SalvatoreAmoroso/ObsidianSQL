using ObsidianSQL.server;
using ObsidianSQL.server.src;
using ObsidianSQL.server.src.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObsidianSQL.test.FakeObjects
{
    class FakeRouter : IRouter
    {
        public IResponse Response { get; set; }
        public IRequest Request { get; set; }
        public Exception ExceptionToThrow { get; set; }
        public bool ThrowException { get; set; } = false;

        public List<Route> Routes { get; set; } = new();

        public IResponse ManageRequest(IRequest request)
        {
            Request = request;
            if (ThrowException)
                throw ExceptionToThrow;
            return Response;
        }

        public void RegisterRoute(Route route)
        {
            Routes.Add(route);
        }

        public void RegisterRoute(ICollection<Route> routes)
        {
            Routes.AddRange(routes);
        }

        public void RemoveRoute(Route route)
        {
            Routes.Remove(route);
        }
    }
}
