namespace ObsidianSQL.library
{
    public interface IDatabase
    {
        public string Name { get; set; }
        public ITable[] Tables { get; }
        public void AddTable(ITable table);
        public void RemoveTable(ITable table);

        public void ExecuteQuery(string query);
    }
}