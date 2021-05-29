using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.src.controller;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.server
{
    class Route
    {
        public readonly string[] Url;

        public readonly Func<IRequest, IResponse> HandleRoute;

        public Route(string[] url, Func<IRequest, IResponse> handleRoute)
        {
            Url = url;
            HandleRoute = handleRoute;
        }
    }
}
