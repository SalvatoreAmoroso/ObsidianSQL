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
			int tableCounter = 0;
			while (tableNameReader.Read())
			{
				var tableName = tableNameReader.GetString(0);
				_tables.Add(new SQLiteTable(_connection, tableName));
			}
			tableNameReader.Close();
		}
		
		public string Name { get; set; }
		public List<ITable> Tables => _tables;
		public void AddTable(ITable table)
		{
			throw new NotImplementedException();
		}

		public void RemoveTable(ITable table)
		{
			throw new NotImplementedException();
		}

		public void ExecuteQuery(string query)
		{
			_connection.ExecuteQuery(query);
		}
	}
}