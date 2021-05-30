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
        public static IConnection CreateConnection(string databaseType, JsonElement connectionData)
        {
            switch (databaseType.ToLower())
            {
                case "sqlite":

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

                default:
                    throw new DatabaseTypeNotFoundException();
            }
        }
    }
}