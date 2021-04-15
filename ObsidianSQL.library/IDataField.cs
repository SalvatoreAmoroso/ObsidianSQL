namespace ObsidianSQL.library
{
    public interface IDataField<T>
    {
        public string ColumnName { get; }
        public T Value { get; }
    }
}