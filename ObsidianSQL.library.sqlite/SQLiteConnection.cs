using System;
using System.Collections.Generic;

namespace ObsidianSQL.library.sqlite
{
	public class SQLiteConnection : IConnection
	{
		private string _filePath;
		internal System.Data.SQLite.SQLiteConnection Connection;
		private List<IDatabase> _databases;

		public SQLiteConnection(string filePath)
		{
			_filePath = filePath;
		}

		public List<IDatabase> Databases => _databases;
		public void AddDatabase(IDatabase database)
		{
			throw new InvalidOperationException("SQLite has only one database");
		}

		public void RemoveDatabase(IDatabase database)
		{
			throw new InvalidOperationException("SQLite must have one database");
		}

		public void Connect()
		{
			Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + _filePath);
			Connection.Open();
			LoadDatabase();
		}

		private void LoadDatabase()
		{
			_databases = new List<IDatabase>();
			_databases.Add(new SQLiteDatabase(this)
			{
				Name = Connection.FileName
			});
		}
		
		public void Disconnect()
		{
			Connection.Close();
		}

		/// <summary>
		/// Executes a query inside the SQLiteConnection
		/// </summary>
		/// <param name="query">SQLCommand to execute</param>
		/// <returns>The number of affected rows</returns>
		public int ExecuteQuery(string query)
		{
			var command = Connection.CreateCommand();
			command.CommandText = query;
			return command.ExecuteNonQuery();
		}
	}
}