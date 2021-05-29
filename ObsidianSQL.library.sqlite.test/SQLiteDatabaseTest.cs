using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			
			Assert.AreEqual(1, connection.Databases.Count);
			
			connection.Disconnect();
		}

		[TestMethod]
		public void TableCountTest()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/database-test.sqlite");
			connection.Connect();
			
			Assert.AreEqual(1, connection.Databases[0].Tables.Count);
			
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