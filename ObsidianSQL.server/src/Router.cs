using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.library;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.http;
using ObsidianSQL.server.src;

namespace ObsidianSQL.server
{
    class Router
    {
        private readonly List<Route> _routes = new();
        private readonly Authentificator _auth;

        public Router(Authentificator auth)
        {
            _auth = auth;
        }

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

        public IResponse Evaluate(IRequest request)
        {
            var route = _routes.Find(x => x.Url == request.Url);

            if(route == null)
            {
                throw new RouteNotFoundException($"The following route is not defined: {request.Url.AbsoluteUri}.");
            }

            IConnection con = _auth.Authentificate(request);

            return route.RouteHandler.GetResponse(request, con);
        }
    }
}