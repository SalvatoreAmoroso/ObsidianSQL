namespace ObsidianSQL.library
{
    public interface IDataField<T>
    {
        public string ColumnName { get; set;  }
        public T Value { get; set; }
    }
}