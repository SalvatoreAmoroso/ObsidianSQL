using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.db;
using ObsidianSQL.server.src.controller;

namespace ObsidianSQL.server
{
    class ObsidianSQL : IDisposable
    {
        private readonly RequestListener _requestListener;

        private readonly Router _router;

        private readonly ConnectionManager _connectionManager;

        public ObsidianSQL(string[] prefixes)
        {
            _router = new Router();
            _requestListener = new RequestListener(prefixes, _router);
            _connectionManager = new ConnectionManager();
            ConfigureRouter();
        }

        public void Dispose()
        {
            _requestListener.Dispose();
        }

        
        private void ConfigureRouter()
        {
            _router.RegisterRoute(new Route(new Uri("login"), new LoginController(_connectionManager)));
            _router.RegisterRoute(new Route(new Uri("http://lol.de/rofl"), new DatabaseController()));
        }
    }
}