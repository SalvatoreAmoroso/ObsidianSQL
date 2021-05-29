using System;

namespace ObsidianSQL.server
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = { "http://lol/" };

            using ObsidianSQL app = new(prefixes);
            app.Start();

            Console.ReadLine();
        }
    }
}