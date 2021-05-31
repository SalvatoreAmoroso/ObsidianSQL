using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.mockup
{
    public class Connection : IConnection
    {
        public List<IDatabase> Databases { get; set; }
        public bool Connected { get; private set; } = false;
        public string ExecutedQuery { get; private set; }

        public void Connect()
        {
            Connected = true;
        }

        public void Disconnect()
        {
            Connected = false;
        }

        public int ExecuteQuery(string query)
        {
            ExecutedQuery = query;
            return 0;
        }

        public void AddDatabase(string database)
        {
            Databases.Add(new Database(database));
        }

        public bool RemoveDatabase(string database)
        {
            return Databases.RemoveAll(db => db.Name == database) > 0;
        }

        public Connection()
        {
            Databases = new List<IDatabase> { new Database("onlineshop"), new Database("onlinegame") };
        }
    }
}
