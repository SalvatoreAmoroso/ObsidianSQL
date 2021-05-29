using System.Collections.Generic;

namespace ObsidianSQL.library
{
    public interface IConnection
    {
        public List<IDatabase> Databases { get; }
        public void AddDatabase(string databaseName);
        public bool RemoveDatabase(string databaseName);
        public void Connect();
        public void Disconnect();
        public int ExecuteQuery(string query);
    }
}