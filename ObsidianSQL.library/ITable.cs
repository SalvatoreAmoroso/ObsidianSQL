namespace ObsidianSQL.library
{
    public interface ITable
    {
        public string Name { get; set; }
        public ITableColumn[] Columns { get; }
        public ITableRow[] GetData(int start, int end);
    }
}