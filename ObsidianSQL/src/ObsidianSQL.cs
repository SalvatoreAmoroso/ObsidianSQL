using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.src.controller;

namespace ObsidianSQL.server.src
{
    class ObsidianSQL : IDisposable
    {
        private readonly RequestListener _requestListener;

        private readonly Router _router;

        public ObsidianSQL(string[] prefixes)
        {
            _requestListener = new RequestListener(prefixes, _router);
            _router = new Router();
            ConfigureRouter();
        }

        public void Dispose()
        {
            _requestListener.Dispose();
        }

        
        private void ConfigureRouter()
        {
            _router.RegisterRoute(new Route(new Uri("http://lol.de/wtf"), new ExampleController()));
            _router.RegisterRoute(new Route(new Uri("http://lol.de/rofl"), new ExampleController()));
        }
    }
}