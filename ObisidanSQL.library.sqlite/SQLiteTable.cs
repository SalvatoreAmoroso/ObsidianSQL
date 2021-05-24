using System.Data;
using ObsidianSQL.library;

namespace ObisidanSQL.library.sqlite
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
			return new ITableRow[0];
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