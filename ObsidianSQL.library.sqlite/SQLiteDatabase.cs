using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ObsidianSQL.library.sqlite
{
	public class SQLiteDatabase : IDatabase
	{
		private SQLiteConnection _connection;
		private List<ITable> _tables;

		public SQLiteDatabase(SQLiteConnection connection)
		{
			_connection = connection;
			LoadTables();
		}

		private void LoadTables()
		{
			_tables = new List<ITable>();
			
			var tableNamesCommand = _connection.Connection.CreateCommand();
			tableNamesCommand.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
			var tableNameReader = tableNamesCommand.ExecuteReader();
			while (tableNameReader.Read())
			{
				var tableName = tableNameReader.GetString(0);
				_tables.Add(new SQLiteTable(_connection, tableName));
			}
			tableNameReader.Close();
		}
		
		public string Name { get; set; }
		public List<ITable> Tables => _tables;
		public void AddTable(string table)
		{
			var command = "CREATE TABLE IF NOT EXISTS '" + table + "' ()";
			
			var tableCommand = _connection.Connection.CreateCommand();
			tableCommand.CommandText = command;
			tableCommand.ExecuteReader();
		}

		public void RemoveTable(string table)
		{
			var command = _connection.Connection.CreateCommand();
			command.CommandText = "DROP TABLE IF EXISTS '" + table + "'";
			command.ExecuteNonQuery();
		}

		public void ExecuteQuery(string query)
		{
			_connection.ExecuteQuery(query);
		}
	}
}