using ObsidianSQL.server.src.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src
{
    interface IRouter
    {
        public void RegisterRoute(Route route);
        public void RegisterRoute(ICollection<Route> routes);
        public void RemoveRoute(Route route);
        public IResponse ManageRequest(IRequest request);

    }
}
