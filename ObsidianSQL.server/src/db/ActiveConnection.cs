using System;
using ObsidianSQL.library;

namespace ObsidianSQL.server.src.db
{
    public class ActiveConnection
    {
        public string Token { get; set; }
        public IConnection Connection { get; set; }
        public DateTime LastUsed { get; set; }

        public ActiveConnection(string token, IConnection connection)
        {
            Token = token;
            Connection = connection;
            LastUsed = DateTime.Now;
        }
    }
}