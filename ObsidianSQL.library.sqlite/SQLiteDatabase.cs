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
			
			var cmd = "SELECT name FROM sqlite_master WHERE type='table'";
			var tableNameReader = QueryHelper.ExecuteDatabaseQuery(_connection, cmd);
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

			QueryHelper.ExecuteDatabaseQuery(_connection, command);
		}

		public bool RemoveTable(string table)
		{
			var command = "DROP TABLE '" + table + "'";
			try
			{
				QueryHelper.ExecuteDatabaseCommand(_connection, command);
				return true;
			}
			catch (SQLiteException)
			{
				return false;
			}
		}

		public void ExecuteQuery(string query)
		{
			QueryHelper.ExecuteDatabaseCommand(_connection, query);
		}
	}
}