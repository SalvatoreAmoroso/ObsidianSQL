using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObisidanSQL.library.sqlite;

namespace ObsidianSQL.library.sqlite.test
{
	[TestClass]
	public class SQLiteTableTest
	{
		[TestMethod]
		public void TableNameChange()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/table-test.sqlite");
			connection.Connect();
			var table = connection.Databases[0].Tables[0];
			table.Name = "NewName";
			
			Assert.AreEqual("NewName", table.Name);
			
			connection.Disconnect();
		}

		[TestMethod]
		public void TableColumnCountTest()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/table-test.sqlite");
			connection.Connect();
			var table = connection.Databases[0].Tables[0];
			
			Assert.AreEqual(2, table.Columns.Length);
			
			connection.Disconnect();
		}
	}
}