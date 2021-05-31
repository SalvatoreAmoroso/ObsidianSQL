using ObsidianSQL.library;
using ObsidianSQL.server.src.db;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.src.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.controller
{
    public abstract class AbstractController
    {
        internal IConnectionManager _connectionManager;
        internal IConnection GetConnection(IRequest request)
        {
            var connection = _connectionManager.GetConnection(request.AuthToken);
            if (connection == null)
            {
                throw new AuthenticationFailedException();
            }
            return connection;
        }
    }
}
