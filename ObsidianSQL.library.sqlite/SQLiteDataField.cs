namespace ObsidianSQL.library.sqlite
{
	public class SQLiteDataField<T> : IDataField<T>
	{
		public SQLiteDataField(string columnName, T value)
		{
			ColumnName = columnName;
			Value = value;
		}
		
		public string ColumnName { get; set; }
		public T Value { get; set; }
	}
}