using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.library;
using ObsidianSQL.server.db;
using ObsidianSQL.server.http;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.server.src.controller
{
    /// <summary>
    /// Mögliches Refactoring: Statt Interface Abstrakten Controller, welche ConnectionManager und Methode um aus Request Connection zu bekommen enthält
    /// </summary>
    public class DatabaseController : IController
    {
        private readonly ConnectionManager _connectionManager;

        public DatabaseController(ConnectionManager con)
        {
            _connectionManager = con;
        }

        public IResponse GetResponse(IRequest request)
        {
            var connection = GetConnection(request);
            
            if(request.HttpMethod.ToLower() == "get")
            {
                return new Response("");
            } else if (request.HttpMethod.ToLower() == "post")
            {
                return new Response("");
            } else
            {
                throw new MethodNotAllowedException();
            }

        }

        private IConnection GetConnection(IRequest request)
        {
            var connection = _connectionManager.GetConnection(request.AuthToken);
            if(connection == null)
            {
                throw new AuthentificationFailedException();
            }
            return connection;
        }
    }
}
