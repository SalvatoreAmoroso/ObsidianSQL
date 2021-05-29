using ObsidianSQL.library;
using ObsidianSQL.server.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ObsidianSQL.server.db;
using System.Dynamic;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.server.src.controller
{
    class LoginController : IController
    {
        private readonly ConnectionManager _connectionManager;

        public LoginController(ConnectionManager con)
        {
            _connectionManager = con;
        }

        public IResponse GetResponse(IRequest request)
        {
            dynamic tokenResponse = new ExpandoObject();
            tokenResponse.token = _connectionManager.CreateConnection(request);

            var json = JsonSerializer.Serialize(tokenResponse);
            return new Response(json);
        }
    }
}
