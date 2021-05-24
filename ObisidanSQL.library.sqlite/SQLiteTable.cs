using ObsidianSQL.library;

namespace ObisidanSQL.library.sqlite
{
	public class SQLiteTable : ITable
	{
		private SQLiteConnection _connection;
		private string _name;

		public SQLiteTable(SQLiteConnection connection, string name)
		{
			_connection = connection;
			_name = name;
		}
		
		public string Name { get => _name; set => ChangeName(value); }
		public ITableColumn[] Columns { get; }
		public ITableRow[] GetData(int start, int end)
		{
			throw new System.NotImplementedException();
		}

		private void ChangeName(string newName)
		{
			_name = newName;
		}
	}
}