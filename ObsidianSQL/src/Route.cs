using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.src
{
    class Route
    {
        public readonly Uri Url;

        public readonly IRouteHandler RouteHandler;

        public Route(Uri url, IRouteHandler routeHandler)
        {
            Url = url;
            RouteHandler = routeHandler;
        }
    }
}
