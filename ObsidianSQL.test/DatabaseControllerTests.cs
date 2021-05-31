using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObsidianSQL.server.src.controller;
using ObsidianSQL.server.src.http;
using ObsidianSQL.test.FakeObjects;

namespace ObsidianSQL.test
{
    [TestClass]
    public class DatabaseControllerTests
    {
        [TestMethod]
        public void GetDatabasesTest()
        {
            FakeConnectionManager fakeManager = new();
            var token = fakeManager.CreateConnection(new JsonElement());
            fakeManager.GetConnection(token).AddDatabase("testdb");

            DatabaseController dbController = new(fakeManager);

            Request request = new()
            {
                AuthToken = token,
                HttpMethod = "get"
            };

            var response = dbController.GetDatabases(request);

            JsonDocument doc = JsonDocument.Parse(response.Content);

            var databaseElements = doc.RootElement.EnumerateArray().ToArray();

            Assert.IsTrue(databaseElements.Length == 1);
            Assert.IsTrue(databaseElements[0].GetProperty("Name").GetString() == "testdb");
        }
    }
}
