using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObsidianSQL.library;
using ObsidianSQL.library.mockup;
using ObsidianSQL.library.sqlite;
using ObsidianSQL.server.src.http;
using ObsidianSQL.server.src.exceptions;
using Serilog;

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
            Log.Debug("Get Connection for " + token);
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
            JObject requestBody;
            try
            {
                requestBody = JObject.Parse(request.HttpBodyContent);
            }
            catch (JsonReaderException)
            {   
                throw new BadRequestException();
            }

            if(!requestBody.TryGetValue("databaseType", out var databaseTypeToken))
            {
                throw new BadRequestException();
            }
            
            IConnection dbConnection = null;
            switch (databaseTypeToken.ToString())
            {
                case "sqlite":
                    if (!requestBody.TryGetValue("filepath", out var filePathToken))
                    {
                        throw new BadRequestException();
                    }
                    try
                    {
                        dbConnection = new SQLiteConnection(filePathToken.ToString());
                    }
                    catch (FileNotFoundException e)
                    {
                        throw new ResourceNotFoundException(e.Message);
                    }
                    break;
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