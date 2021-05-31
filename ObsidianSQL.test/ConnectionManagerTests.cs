using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObsidianSQL.library;
using ObsidianSQL.library.mockup;
using ObsidianSQL.server.src.db;
using ObsidianSQL.test.FakeObjects;

namespace ObsidianSQL.test
{
    [TestClass]
    public class ConnectionManagerTests
    {

        [TestMethod]
        public void CreateConnectionTest()
        {
            ConnectionManager connectionManager = new(new FakeConnectionFactory());
            dynamic jsonObj = new ExpandoObject();
            jsonObj.databaseType = "testType";
            var doc = JsonDocument.Parse(JsonSerializer.Serialize(jsonObj));

            var token = connectionManager.AddConnection(doc.RootElement);

            Connection connection = connectionManager.GetConnection(token);

            Assert.IsNotNull(connection);
            Assert.IsTrue(connection.Connected);
        }
    }
}