using ObsidianSQL.library;
using ObsidianSQL.server.src.exceptions;
using System;
using System.Collections.Generic;
using ObsidianSQL.library.sqlite;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace ObsidianSQL.server.src.db
{
    public class DBConnectionFactory
    {
        private static readonly Dictionary<string, Func<JsonElement, IConnection>> Connections = new()
        {
            { "sqlite", CreateSQLiteConnection }
        };

        public static IConnection CreateConnection(string databaseType, JsonElement connectionData)
        {
            var CreateConnection = Connections[databaseType];

            if(CreateConnection == null)
            {
                throw new DatabaseTypeNotFoundException();
            }

            return CreateConnection.Invoke(connectionData);
        }


        private static IConnection CreateSQLiteConnection(JsonElement connectionData)
        {
            if (!connectionData.TryGetProperty("filepath", out var filePathToken))
            {
                throw new BadRequestException();
            }

            IConnection connection = null;
            try
            {
                connection = new SQLiteConnection(filePathToken.GetString());
            }
            catch (FileNotFoundException ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
            return connection;
        }
    }
}