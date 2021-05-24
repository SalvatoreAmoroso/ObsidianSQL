using ObsidianSQL.library;

namespace ObisidanSQL.library.sqlite
{
	public class SQLiteTableColumn : ITableColumn
	{
		public string Name { get; set; }
		public string Datatype { get; set; }
	}
}