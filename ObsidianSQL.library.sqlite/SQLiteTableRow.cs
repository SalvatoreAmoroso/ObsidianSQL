using System.Collections.Generic;

namespace ObsidianSQL.library.sqlite
{
	public class SQLiteTableRow : ITableRow
	{
		private List<IDataField<object>> _fields = new();

		internal void AddDataField(SQLiteDataField<object> field)
		{
			_fields.Add(field);
		}
		
		public IDataField<T> GetDataField<T>(string column)
		{
			return _fields.Find(field => field.ColumnName == column) as SQLiteDataField<T>;
		}

		public List<IDataField<object>> DataFields => _fields;
	}
}