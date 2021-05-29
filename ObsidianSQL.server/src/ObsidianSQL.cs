using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.db;
using ObsidianSQL.server.src.controller;
using Serilog;

namespace ObsidianSQL.server
{
    class ObsidianSQL : IDisposable
    {
        private readonly RequestListener _requestListener;

        private readonly Router _router;

        private readonly ConnectionManager _connectionManager;

        public ObsidianSQL(string[] prefixes)
        {
            ConfigureLogger();
            _router = new Router();
            _requestListener = new RequestListener(prefixes, _router);
            _connectionManager = new ConnectionManager();
            ConfigureRouter();
        }

        public void Dispose()
        {
            _requestListener.Stop();
            _requestListener.Dispose();
        }

        public void Start()
        {
            _requestListener.HandleRequests();
        }

        private void ConfigureRouter()
        {
            _router.RegisterRoute(new Route(new Uri("http://localhost:8080/login"), new LoginController(_connectionManager)));
            _router.RegisterRoute(new Route(new Uri("http://localhost:8080/databases"), new DatabaseController(_connectionManager)));
        }

        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#endif
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}