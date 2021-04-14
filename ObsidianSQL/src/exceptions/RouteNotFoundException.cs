using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
