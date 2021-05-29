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
        private readonly LoginController _loginController;
        private readonly DatabaseController _dbController;
        private readonly TableController _tableController;

        public ObsidianSQL(string[] prefixes)
        {
            ConfigureLogger();
            _router = new Router();
            _requestListener = new RequestListener(prefixes, _router);
            _connectionManager = new ConnectionManager();
            _loginController = new LoginController(_connectionManager);
            _dbController = new DatabaseController(_connectionManager);
            _tableController = new TableController(_connectionManager);
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
            _router.RegisterRoute(new Route(new string[] {"login"}, _loginController.Login));
            _router.RegisterRoute(new Route(new string[] {"databases"}, _dbController.GetDatabases));
            _router.RegisterRoute(new Route(new string[] {"database", "*"}, _dbController.GetDatabaseInfo));
            
            _router.RegisterRoute(new Route(new string[] {"database", "*", "table", "*"}, _tableController.GetTableInfo));
            _router.RegisterRoute(new Route(new string[] {"database", "*", "table", "*", "data"}, _tableController.GetTableData));
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