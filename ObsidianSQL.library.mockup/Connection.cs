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
        public void Connect()
        {
            Console.WriteLine($"Connected from DB.");
        }

        public void Disconnect()
        {
            Console.WriteLine($"Disconnected from DB.");
        }

        public int ExecuteQuery(string query)
        {
            Console.WriteLine($"{query} executed.");
            return 0;
        }

        public void AddDatabase(IDatabase database)
        {
            Databases.Add(database);
        }

        public void RemoveDatabase(IDatabase database)
        {
            Databases.Remove(database);
        }

        public Connection()
        {
            Databases = new List<IDatabase> { new Database("onlineshop"), new Database("onlinegame") };
        }
    }
}
