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
        public readonly string[] Url;

        public readonly IController RouteHandler;

        public Route(string[] url, IController routeHandler)
        {
            Url = url;
            RouteHandler = routeHandler;
        }
    }
}
