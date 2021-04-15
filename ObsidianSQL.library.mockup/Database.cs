using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.mockup
{
    public class Database : IDatabase
    {
        public string Name { get; set; }

        public ITable[] Tables { get; set; }

        public void ExecuteQuery(string query)
        {
            Console.WriteLine($"{query} executed.");
        }

        public Database(string name)
        {
            Name = name;
            Tables = new Table[] { new Table("Users"), new Table("Presidents") };
        }
    }
}
