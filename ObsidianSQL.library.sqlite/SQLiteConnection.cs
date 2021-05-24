using System;

namespace ObsidianSQL.library.sqlite
{
	public class SQLiteConnection : IConnection
	{
		private string _filePath;
		internal System.Data.SQLite.SQLiteConnection Connection;
		private SQLiteDatabase[] _databases;

		public SQLiteConnection(string filePath)
		{
			_filePath = filePath;
		}

		public IDatabase[] Databases => _databases;

		public void Connect()
		{
			Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + _filePath);
			Connection.Open();
			LoadDatabase();
		}

		private void LoadDatabase()
		{
			_databases = new SQLiteDatabase[1];
			_databases[0] = new SQLiteDatabase(this)
			{
				Name = Connection.FileName
			};
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