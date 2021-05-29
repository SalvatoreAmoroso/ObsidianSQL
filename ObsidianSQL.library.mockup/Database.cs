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

        public void AddTable(string tableName)
        {
            Tables.Add(new Table(tableName));
        }

        public bool RemoveTable(string tableName)
        {
            return Tables.RemoveAll(table => table.Name == tableName) > 0;
        }

        public Database(string name)
        {
            Name = name;
            Tables = new List<ITable> { new Table("Users"), new Table("Products") };
        }
    }
}
