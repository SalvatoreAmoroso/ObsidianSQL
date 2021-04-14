namespace ObsidianSQL.library
{
    public interface ITable
    {
        public string Name { get; set; }
        public ITableColumn[] Columns { get; }
        public string Data { get; set; }
    }
}