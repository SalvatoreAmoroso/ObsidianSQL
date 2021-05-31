using ObsidianSQL.server.src.db;
using System;

namespace ObsidianSQL.server
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = { "http://*:8080/" };

            using ObsidianSQL app = new(prefixes, new ConnectionManager(new DBConnectionFactory()));
            app.Start();

            Console.ReadLine();
        }
    }
}