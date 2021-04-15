namespace ObsidianSQL.library
{
    public interface IDatabase
    {
        public string Name { get; set; }
        public ITable[] Tables { get; set;  }

        public void ExecuteQuery(string query);
    }
}