using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObsidianSQL.library.sqlite.test
{
	[TestClass]
	public class SQLiteConnectionTest
	{
		[TestMethod]
		public void ExecuteInsertCommandTest()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/connection-test.sqlite");
			connection.Connect();
			int rows = connection.ExecuteQuery("INSERT INTO TestData(TestString, TestInteger) VALUES('test', 8)");

			Assert.AreEqual(1, rows);
			
			connection.Disconnect();
		}
		
		[TestMethod]
		public void ExecuteDeleteCommandTest()
		{
			SQLiteConnection connection = new SQLiteConnection("TestDatabase/connection-test.sqlite");
			connection.Connect();
			int rows = connection.ExecuteQuery("DELETE FROM TestData WHERE TestInteger = 2");

			Assert.AreEqual(1, rows);
			
			connection.Disconnect();
		}
	}
}