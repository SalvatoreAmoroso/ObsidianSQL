using ObsidianSQL.library;
using ObsidianSQL.library.mockup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.test.FakeObjects
{
    class FakeConnection : IConnection
    {
        public List<IDatabase> Databases { get; }
        public bool isConnected = false;
        public bool isDisconnected = true;
        public string query = "";

        public void AddDatabase(string databaseName)
        {
            Databases.Add(new Database(databaseName));
        }

        public bool RemoveDatabase(string databaseName)
        {
            return Databases.RemoveAll(db => db.Name == databaseName) > 0;
        }

        public void Connect()
        {
            isDisconnected = false;
            isConnected = true;
        }

        public void Disconnect()
        {
            isDisconnected = true;
            isConnected = false;
        }

        public int ExecuteQuery(string query)
        {
            this.query = query;
            return 1;
        }
    }
}
