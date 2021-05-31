﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObsidianSQL.server.src.controller;
using ObsidianSQL.server.src.http;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.test.FakeObjects;

namespace ObsidianSQL.test
{
    [TestClass]
    public class DatabaseControllerTests
    {
        [TestMethod]
        public void GetDatabasesTest()
        {
            //Arrange
            FakeConnectionManager fakeManager = new();
            var token = fakeManager.CreateConnection(new JsonElement());
            fakeManager.GetConnection(token).AddDatabase("testdb");

            DatabaseController dbController = new(fakeManager);

            Request request = new()
            {
                AuthToken = token,
                HttpMethod = "get"
            };

            //Act
            var response = dbController.GetDatabases(request);

            //Assert
            JsonDocument doc = JsonDocument.Parse(response.Content);
            var databaseElements = doc.RootElement.EnumerateArray().ToArray();

            Assert.IsTrue(response.HttpStatusCode == 200);
            Assert.IsTrue(databaseElements.Length == 1);
            Assert.IsTrue(databaseElements[0].GetProperty("Name").GetString() == "testdb");
        }

        [TestMethod]
        public void CreateDatabaseTest()
        {
            //Arrange
            FakeConnectionManager fakeManager = new();
            var token = fakeManager.CreateConnection(new JsonElement());
            var connection = fakeManager.GetConnection(token);

            DatabaseController dbController = new(fakeManager);

            dynamic reqBody = new ExpandoObject();
            reqBody.databaseName = "newDatabase";

            Request request = new()
            {
                AuthToken = token,
                HttpMethod = "post",
                HttpBodyContent = JsonSerializer.Serialize(reqBody)
            };

            //Act
            var response = dbController.CreateDatabase(request);

            //Assert
            Assert.IsTrue(response.HttpStatusCode == 201);
            Assert.IsNotNull(connection.Databases.Find(db => db.Name == "newDatabase"));
        }

        [TestMethod]
        public void DeleteDatabaseTest()
        {
            //Arrange
            FakeConnectionManager fakeManager = new();
            var token = fakeManager.CreateConnection(new JsonElement());
            var connection = fakeManager.GetConnection(token);
            DatabaseController dbController = new(fakeManager);

            connection.AddDatabase("testdb");
            connection.AddDatabase("testdbToDelete");

            Request request = new()
            {
                AuthToken = token,
                HttpMethod = "delete",
                UrlPlaceholderValues = new List<string>{ "testdbToDelete" }
            };


            //Act
            var response = dbController.DeleteDatabase(request);


            Assert.IsTrue(connection.Databases.Count == 1);
            Assert.IsTrue(response.HttpStatusCode == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException), "A database was deleted, which does not exist.")]
        public void DeleteDatabaseThatNotExistTest()
        {
            //Arrange
            FakeConnectionManager fakeManager = new();
            var token = fakeManager.CreateConnection(new JsonElement());
            var connection = fakeManager.GetConnection(token);
            DatabaseController dbController = new(fakeManager);

            connection.AddDatabase("testdb");
            connection.AddDatabase("testdbToDelete");

            Request request = new()
            {
                AuthToken = token,
                HttpMethod = "delete",
                UrlPlaceholderValues = new List<string> { "testDbThatNotExist" }
            };


            //Act
            dbController.DeleteDatabase(request);
        }
    }
}