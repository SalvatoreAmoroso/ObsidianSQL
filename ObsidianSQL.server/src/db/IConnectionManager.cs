using ObsidianSQL.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.db
{
    public interface IConnectionManager
    {
        /// <summary>
        /// Get an existing connection by token
        /// </summary>
        /// <param name="token">Token to search for an existing connection</param>
        /// <returns></returns>
        public IConnection GetConnection(string token);

        /// <summary>
        /// Create a new connection to the database
        /// </summary>
        /// <param name="connectionData">JSON, which contains all necessary data for authentication (e.g. username and password)</param>
        /// <returns>Token</returns>
        public string AddConnection(JsonElement connectionData);
    }
}