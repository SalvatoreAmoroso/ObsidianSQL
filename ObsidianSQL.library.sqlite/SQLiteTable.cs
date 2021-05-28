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

		public SQLiteTable(SQLiteConnection connection, string name)
		{
			_connection = connection;
			_name = name;
			LoadColumns();
		}
		
		public string Name { get => _name; set => ChangeName(value); }
		public ITableColumn[] Columns => _columns;
		public ITableRow[] GetData(int start, int end)
		{
			var readCommand = _connection.Connection.CreateCommand();
			readCommand.CommandText = "SELECT * FROM '" + _name + "' LIMIT " + start + ", " + (end - start);
			var reader = readCommand.ExecuteReader();

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
			}
			return result;
		}

		private void ChangeName(string newName)
		{
			var command = _connection.Connection.CreateCommand();
			command.CommandText = "ALTER TABLE '"+_name+"' RENAME TO '"+newName+"'";
			command.ExecuteNonQuery();
			_name = newName;
		}

		private void LoadColumns()
		{
			var tableColumnCountCommand = _connection.Connection.CreateCommand();
			tableColumnCountCommand.CommandText = "SELECT * FROM '" + _name + "' LIMIT 1";
			var tableColumnCountReader = tableColumnCountCommand.ExecuteReader();
			int tableColumnsLength = tableColumnCountReader.FieldCount;
			_columns = new SQLiteTableColumn[tableColumnsLength];
			tableColumnCountReader.Close();

			var tableColumnCommand = _connection.Connection.CreateCommand();
			tableColumnCommand.CommandText = "PRAGMA table_info(" + _name + ")";
			var tableColumnReader = tableColumnCommand.ExecuteReader();
			
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