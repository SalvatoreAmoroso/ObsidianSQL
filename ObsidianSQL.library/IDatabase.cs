namespace ObsidianSQL.library
{
    public interface IDatabase
    {
        public string Name { get; set; }
        public ITable[] Tables { get; }

        public void ExecuteQuery(string query);
    }
}