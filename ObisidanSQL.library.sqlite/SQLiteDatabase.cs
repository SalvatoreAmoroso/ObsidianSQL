using ObsidianSQL.library;

namespace ObisidanSQL.library.sqlite
{
	public class SQLiteDatabase : IDatabase
	{
		public string Name { get; set; }
		public ITable[] Tables { get; }

		public void ExecuteQuery(string query)
		{
			throw new System.NotImplementedException();
		}
	}
}