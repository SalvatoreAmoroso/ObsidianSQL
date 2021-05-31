using ObsidianSQL.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.db
{
    public interface IConnectionFactory
    {
        public IConnection CreateConnection(string databaseType, JsonElement connectionData);
    }
}
