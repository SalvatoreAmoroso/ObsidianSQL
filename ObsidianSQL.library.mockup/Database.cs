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

        public List<ITable> Tables { get; set; }

        public void ExecuteQuery(string query)
        {
            Console.WriteLine($"{query} executed.");
        }

        public void AddTable(ITable table)
        {
            Tables.Add(table);
        }

        public void RemoveTable(ITable table)
        {
            Tables.Remove(table);
        }

        public Database(string name)
        {
            Name = name;
            Tables = new List<ITable> { new Table("Users"), new Table("Products") };
        }
    }
}
