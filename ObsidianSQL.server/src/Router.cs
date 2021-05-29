using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.library;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.server
{
    class Router
    {
        private readonly List<Route> _routes = new();

        public void RegisterRoute(Route route)
        {
            _routes.Add(route);
        }

        public void RegisterRoute(ICollection<Route> routes)
        {
            foreach (var route in routes)
            {
                _routes.Add(route);
            }
        }

        public void RemoveRoute(Route route)
        {
            _routes.Remove(route);
        }

        public IResponse ManageRequest(IRequest request)
        {
            var route = _routes.Find(x => x.Url == request.Url);

            if(route == null)
            {
                throw new RouteNotFoundException($"The following route is not defined: {request.Url.AbsoluteUri}.");
            }

            return route.RouteHandler.GetResponse(request);
        }
    }
}