using System;
using System.Data.SQLite;
using ObsidianSQL.library;

namespace ObisidanSQL.library.sqlite
{
	public class SQLiteDatabase : IDatabase
	{
		private SQLiteConnection _connection;
		private SQLiteTable[] _tables;

		public SQLiteDatabase(SQLiteConnection connection)
		{
			_connection = connection;
			LoadTables();
		}

		private void LoadTables()
		{
			var tableLengthCommand = _connection.Connection.CreateCommand();
			tableLengthCommand.CommandText = "SELECT COUNT() FROM sqlite_master WHERE type='table'";
			var tableLengthReader = tableLengthCommand.ExecuteReader();
			tableLengthReader.Read();
			int tableLength = tableLengthReader.GetInt32(0);
			tableLengthReader.Close();
			_tables = new SQLiteTable[tableLength];
			
			var tableNamesCommand = _connection.Connection.CreateCommand();
			tableNamesCommand.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
			var tableNameReader = tableNamesCommand.ExecuteReader();
			int tableCounter = 0;
			while (tableNameReader.Read())
			{
				var tableName = tableNameReader.GetString(0);
				_tables[tableCounter++] = new SQLiteTable(_connection, tableName);
			}
			tableNameReader.Close();
		}
		
		public string Name { get; set; }
		public ITable[] Tables => _tables;

		public void ExecuteQuery(string query)
		{
			_connection.ExecuteQuery(query);
		}
	}
}