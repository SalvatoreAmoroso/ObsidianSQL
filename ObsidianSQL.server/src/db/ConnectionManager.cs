using System;
using System.Collections.Generic;
using ObsidianSQL.library;
using ObsidianSQL.library.mockup;
using ObsidianSQL.server.http;

namespace ObsidianSQL.server.db
{
    public class ConnectionManager
    {
        private List<ActiveConnection> _connections;

        public ConnectionManager()
        {
            _connections = new List<ActiveConnection>();
        }
        
        /// <summary>
        /// Get an existing connection from a given token
        /// </summary>
        public IConnection GetConnection(string token)
        {
            ActiveConnection connection = _connections.Find(conn => conn.Token == token);
            return connection?.Connection;
        }

        /// <summary>
        /// Create a connection for a new user
        /// </summary>
        /// <param name="request">The request from the user</param>
        /// <returns>The token for the user</returns>
        public string CreateConnection(IRequest request)
        {
            //TODO: Create the right connection (SQLite, MySQL, ..) based on the request
            IConnection dbConnection = new Connection();
            string token = GenerateToken();
            _connections.Add(new ActiveConnection(token, dbConnection));
            
            return token;
        }

        /// <summary>
        /// Generate a new random token
        /// </summary>
        private string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}