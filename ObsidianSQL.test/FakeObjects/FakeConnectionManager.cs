using ObsidianSQL.library;
using ObsidianSQL.library.mockup;
using ObsidianSQL.server.src.db;
using ObsidianSQL.server.src.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObsidianSQL.test.FakeObjects
{
    class FakeConnectionManager : IConnectionManager
    {
        public List<ActiveConnection> Connections { get; set; } = new();
        private int counter = 0;

        public string CreateConnection(JsonElement connectionData)
        {
            string token = $"fakeToken{++counter}";
            Connections.Add(new ActiveConnection(token, new Connection()));
            return token;
        }

        public IConnection GetConnection(string token)
        {
            return Connections.Find(connection => connection.Token == token)?.Connection;
        }
    }
}
