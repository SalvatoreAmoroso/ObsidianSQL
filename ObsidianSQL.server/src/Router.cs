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
            string[] urlFragments = request.Url.Segments;
            
            var route = _routes.Find(r => RouteMatches(r, urlFragments));
            request.UrlPlaceholderValues = GetPlaceholderValues(route, urlFragments);

            if(route == null)
            {
                throw new RouteNotFoundException($"The following route is not defined: {request.Url.AbsoluteUri}.");
            }

            return route.RouteHandler.GetResponse(request);
        }

        private bool RouteMatches(Route route, string[] urlFragments)
        {
            //Skip first slash
            if (urlFragments.Length > 0 && urlFragments[0] == "/")
                urlFragments = urlFragments.Skip(1).ToArray();
            
            for (int i = 0; i < urlFragments.Length; i++)
            {
                if (urlFragments[i].EndsWith("/"))
                    urlFragments[i] = urlFragments[i].Remove(urlFragments[i].Length - 1); //remove trailing slashes
                if (i >= route.Url.Length)
                    return false;
                if (route.Url[i] == "*")
                    continue;
                if (urlFragments[i] != route.Url[i])
                    return false;
            }
            return true;
        }

        private List<string> GetPlaceholderValues(Route route, string[] urlFragments)
        {
            List<string> values = new();
            
            //Skip first slash
            if (urlFragments.Length > 0 && urlFragments[0] == "/")
                urlFragments = urlFragments.Skip(1).ToArray();
            
            for (int i = 0; i < urlFragments.Length && i < route.Url.Length; i++)
            {
                if (urlFragments[i].EndsWith("/"))
                    urlFragments[i] = urlFragments[i].Remove(urlFragments[i].Length - 1); //remove trailing slashes
                if (route.Url[i] == "*")
                    values.Add(urlFragments[i]);
            }

            return values;
        }
    }
}