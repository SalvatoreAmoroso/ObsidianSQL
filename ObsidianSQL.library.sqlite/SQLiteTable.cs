using System;
using System.Data;
using System.Reflection;

namespace ObsidianSQL.library.sqlite
{
	public class SQLiteTable : ITable
	{
		private SQLiteConnection _connection;
		private string _name;
		private SQLiteTableColumn[] _columns;
		private QueryHelper _queryHelper;

		public SQLiteTable(SQLiteConnection connection, QueryHelper queryHelper, string name)
		{
			_connection = connection;
			_name = name;
			_queryHelper = queryHelper;
			LoadColumns();
		}
		
		public string Name { get => _name; set => ChangeName(value); }
		public ITableColumn[] Columns => _columns;
		public ITableRow[] GetData(int start, int end)
		{
			var readCommand = "SELECT * FROM '" + _name + "' LIMIT " + start + ", " + (end - start);
			var reader = _queryHelper.ExecuteDatabaseQuery(_connection, readCommand);

			SQLiteTableRow[] result = new SQLiteTableRow[end - start + 1];
			int resultCounter = 0;
			while (reader.Read())
			{
				result[resultCounter] = new SQLiteTableRow();
				for (int col = 0; col < reader.FieldCount; col++)
				{
					var dataField = new SQLiteDataField<object>(
						reader.GetOriginalName(col),
						reader.GetValue(col)
						);
					result[resultCounter].AddDataField(dataField);
				}

				resultCounter++;
			}
			return result;
		}

		private void ChangeName(string newName)
		{
			var command = "ALTER TABLE '"+_name+"' RENAME TO '"+newName+"'";
			_queryHelper.ExecuteDatabaseCommand(_connection, command);
			_name = newName;
		}

		private void LoadColumns()
		{
			var tableColumnCountCommand = "SELECT * FROM '" + _name + "' LIMIT 1";
			var tableColumnCountReader = _queryHelper.ExecuteDatabaseQuery(_connection, tableColumnCountCommand);
			int tableColumnsLength = tableColumnCountReader.FieldCount;
			_columns = new SQLiteTableColumn[tableColumnsLength];
			tableColumnCountReader.Close();

			var tableColumnCommand = "PRAGMA table_info(" + _name + ")";
			var tableColumnReader = _queryHelper.ExecuteDatabaseQuery(_connection, tableColumnCommand);
			
			int tableColumnCounter = 0;
			while (tableColumnReader.Read())
			{
				_columns[tableColumnCounter++] = new SQLiteTableColumn()
				{
					Datatype = tableColumnReader.GetString("type"),
					Name = tableColumnReader.GetString("name")
				};
			}
			tableColumnReader.Close();
		}
	}
}