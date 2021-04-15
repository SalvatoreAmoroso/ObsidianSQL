using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.mockup
{
    public class Connection : IConnection
    {
        public IDatabase[] Databases { get; set; }

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


        public Connection()
        {
            Databases = new Database[] { new Database("onlineshop"), new Database("onlinegame") };
        }
    }
}
