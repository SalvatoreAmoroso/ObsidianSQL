using System;
using ObsidianSQL.library;

namespace ObisidanSQL.library.sqlite
{
	public class SQLiteConnection : IConnection
	{
		private string _filePath;
		private System.Data.SQLite.SQLiteConnection _connection;
		private SQLiteDatabase _database;

		public SQLiteConnection(string filePath)
		{
			_filePath = filePath;
		}

		public IDatabase[] Databases { get; }

		public void Connect()
		{
			_connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + _filePath);
			_connection.Open();
		}

		public void Disconnect()
		{
			_connection.Close();
		}

		/// <summary>
		/// Executes a query inside the SQLiteConnection
		/// </summary>
		/// <param name="query">SQLCommand to execute</param>
		/// <returns>The number of affected rows</returns>
		public int ExecuteQuery(string query)
		{
			var command = _connection.CreateCommand();
			command.CommandText = query;
			return command.ExecuteNonQuery();
		}
	}
}