using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.src.controller;

namespace ObsidianSQL.server
{
    class Route
    {
        public readonly Uri Url;

        public readonly IController RouteHandler;

        public Route(Uri url, IController routeHandler)
        {
            Url = url;
            RouteHandler = routeHandler;
        }
    }
}
