namespace ObsidianSQL.library
{
    public interface IConnection
    {
        public IDatabase[] Databases { get; }
        public void AddDatabase(IDatabase database);
        public void RemoveDatabase(IDatabase database);
        public void Connect();
        public void Disconnect();
        public int ExecuteQuery(string query);
    }
}