using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            FakeConnectionManager fakeConnectionManager = new();

            var token1 = fakeConnectionManager.CreateConnection(new JsonElement());
            var token2 = fakeConnectionManager.CreateConnection(new JsonElement());

            Assert.IsTrue(fakeConnectionManager.Connections.Count == 2);
            Assert.IsTrue(fakeConnectionManager.Connections[0].Token == token1);
            Assert.IsTrue(fakeConnectionManager.Connections[1].Token == token2);
        }

        [TestMethod]
        public void GetConnectionTest()
        {
            FakeConnectionManager fakeConnectionManager = new();
            fakeConnectionManager.Connections.Add(new ActiveConnection("token1", new Connection()));

            var connection = fakeConnectionManager.GetConnection("token1");

            Assert.IsNotNull(connection);
        }
    }
}