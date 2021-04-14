namespace ObsidianSQL.library
{
    public interface IConnection
    {
        public IDatabase[] Databases { get; }
        public void Connect();
        public void Disconnect();
        public int ExecuteQuery(string query);
    }
}