using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.src.db;
using ObsidianSQL.server.src.controller;
using ObsidianSQL.server.src.http;
using Serilog;

namespace ObsidianSQL.server
{
    class ObsidianSQL : IDisposable
    {
        private readonly IHttpListener _httpListener;
        private readonly RequestListener _requestListener;
        private readonly Router _router;
        private readonly LoginController _loginController;
        private readonly DatabaseController _dbController;
        private readonly TableController _tableController;

        public ObsidianSQL(string[] prefixes, IConnectionManager connectionManager)
        {
            ConfigureLogger();
            _router = new Router();
            _httpListener = new HttpListener();
            _requestListener = new RequestListener(_httpListener, prefixes, _router);
            _loginController = new LoginController(connectionManager);
            _dbController = new DatabaseController(connectionManager);
            _tableController = new TableController(connectionManager);
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
            _router.RegisterRoute(new Route(new string[] { "login" }, _loginController.Login));
            _router.RegisterRoute(new Route(new string[] { "databases" }, _dbController.GetDatabases));
            _router.RegisterRoute(new Route(new string[] { "database", "*" }, _dbController.GetDatabaseInfo));
            _router.RegisterRoute(new Route(new string[] { "deleteDatabase", "*" }, _dbController.DeleteDatabase));
            _router.RegisterRoute(new Route(new string[] { "createDatabase" }, _dbController.CreateDatabase));

            _router.RegisterRoute(new Route(new string[] { "database", "*", "table", "*" }, _tableController.GetTableInfo));
            _router.RegisterRoute(new Route(new string[] { "database", "*", "table", "*", "data" }, _tableController.GetTableData));
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