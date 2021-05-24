using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObisidanSQL.library.sqlite;

namespace ObsidianSQL.library.sqlite.test
{
	[TestClass]
	public class SQLiteDatabaseTest
	{
		[TestMethod]
		public void DatabaseCountTest()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/database-test.sqlite");
			connection.Connect();
			
			Assert.AreEqual(1, connection.Databases.Length);
			
			connection.Disconnect();
		}

		[TestMethod]
		public void TableCountTest()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/database-test.sqlite");
			connection.Connect();
			
			Assert.AreEqual(1, connection.Databases[0].Tables.Length);
			
			connection.Disconnect();
		}
		
		[TestMethod]
		public void TableNameTest()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/database-test.sqlite");
			connection.Connect();
			
			Assert.AreEqual("TestData", connection.Databases[0].Tables[0].Name);
			
			connection.Disconnect();
		}
	}
}