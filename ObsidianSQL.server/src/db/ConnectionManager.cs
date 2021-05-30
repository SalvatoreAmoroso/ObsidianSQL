using System;
using System.Collections.Generic;
using System.IO;
using ObsidianSQL.library;
using ObsidianSQL.library.mockup;
using ObsidianSQL.library.sqlite;
using ObsidianSQL.server.src.http;
using ObsidianSQL.server.src.exceptions;
using Serilog;
using System.Text.Json;

namespace ObsidianSQL.server.src.db
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly List<ActiveConnection> _connections;

        public ConnectionManager()
        {
            _connections = new List<ActiveConnection>();
        }
        
        /// <summary>
        /// Get an existing connection from a given token
        /// </summary>
        public IConnection GetConnection(string token)
        {
            Log.Debug("Get Connection for " + token);
            ActiveConnection connection = _connections.Find(conn => conn.Token == token);
            return connection?.Connection;
        }

        /// <summary>
        /// Create a connection for a new user
        /// </summary>
        /// <param name="request">The request from the user</param>
        /// <returns>The token for the user</returns>
        public string CreateConnection(JsonElement connectionData)
        {
            if(!connectionData.TryGetProperty("databaseType", out var databaseTypeToken))
            {
                throw new BadRequestException();
            }
            
            IConnection dbConnection = null;
            switch (databaseTypeToken.GetString())
            {
                case "sqlite":
                    if (!connectionData.TryGetProperty("filepath", out var filePathToken))
                    {
                        throw new BadRequestException();
                    }
                    try
                    {
                        dbConnection = new SQLiteConnection(filePathToken.GetString());
                    }
                    catch (FileNotFoundException e)
                    {
                        throw new ResourceNotFoundException(e.Message);
                    }
                    break;
            }

            if(dbConnection == null)
            {
                throw new DatabaseTypeNotFoundException(); 
            }

            dbConnection.Connect();
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