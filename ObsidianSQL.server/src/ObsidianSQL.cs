using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.controller;
using ObsidianSQL.server.src;

namespace ObsidianSQL.server
{
    class ObsidianSQL : IDisposable
    {
        private readonly RequestListener _requestListener;

        private readonly Router _router;

        private readonly Authentificator _auth;

        public ObsidianSQL(string[] prefixes)
        {
            _auth = new Authentificator();
            _router = new Router(_auth);
            _requestListener = new RequestListener(prefixes, _router);
            ConfigureRouter();
        }

        public void Dispose()
        {
            _requestListener.Dispose();
        }

        
        private void ConfigureRouter()
        {
            _router.RegisterRoute(new Route(new Uri("http://lol.de/wtf"), new DatabaseController()));
            _router.RegisterRoute(new Route(new Uri("http://lol.de/rofl"), new DatabaseController()));
        }
    }
}