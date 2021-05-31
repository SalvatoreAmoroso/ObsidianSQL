using ObsidianSQL.library;
using ObsidianSQL.library.mockup;
using ObsidianSQL.server.src.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObsidianSQL.test.FakeObjects
{
    public class FakeConnectionFactory : IConnectionFactory
    {
        public IConnection CreateConnection(string databaseType, JsonElement connectionData)
        {
            return new Connection();
        }
    }
}
