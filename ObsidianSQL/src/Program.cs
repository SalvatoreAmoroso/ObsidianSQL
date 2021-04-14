using System;

namespace ObsidianSQL.server.src
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = { "http://lol/" };

            using ObsidianSQL app = new(prefixes);

            Console.ReadLine();
        }
    }
}