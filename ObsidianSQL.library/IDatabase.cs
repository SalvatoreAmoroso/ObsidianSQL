using System.Collections.Generic;

namespace ObsidianSQL.library
{
    public interface IDatabase
    {
        public string Name { get; set; }
        public List<ITable> Tables { get; }
        public void AddTable(string tableName);
        public bool RemoveTable(string tableName);
        public void ExecuteQuery(string query);
    }
}