using System;

namespace ObsidianSQL.server.src.exceptions
{
    class RouteNotFoundException : Exception
    {
        public RouteNotFoundException()
        {
        }

        public RouteNotFoundException(string message)
            : base(message)
        {
        }

        public RouteNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}