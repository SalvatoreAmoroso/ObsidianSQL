namespace ObsidianSQL.library.sqlite
{
	public class SQLiteDataField<T> : IDataField<T>
	{
		public SQLiteDataField(string columnName, T value)
		{
			ColumnName = columnName;
			Value = value;
		}
		
		public string ColumnName { get; }
		public T Value { get; }
	}
}