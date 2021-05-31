using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.library;
using ObsidianSQL.server.db;
using ObsidianSQL.server.src.http;
using ObsidianSQL.server.src.exceptions;
using System.Text.Json;

namespace ObsidianSQL.server.src.controller
{
    /// <summary>
    /// Mögliches Refactoring: Statt Interface Abstrakten Controller, welche ConnectionManager und Methode um aus Request Connection zu bekommen enthält
    /// </summary>
    public class DatabaseController
    {
        private readonly ConnectionManager _connectionManager;

        public DatabaseController(ConnectionManager con)
        {
            _connectionManager = con;
        }

        public IResponse GetDatabases(IRequest request)
        {
            var connection = GetConnection(request);

            if (request.HttpMethod != "get")
            {
                throw new MethodNotAllowedException();
            }

            var databases = connection.Databases;

            return new Response(JsonSerializer.Serialize(databases), 200);
        }

        public IResponse CreateDatabase(IRequest request)
        {
            var connection = GetConnection(request);

            if(request.HttpMethod != "post")
            {
                throw new MethodNotAllowedException();
            }

            JsonElement databaseData;

            try
            {
                databaseData = JsonDocument.Parse(request.HttpBodyContent).RootElement;
            }
            catch (JsonException)
            {
                throw new BadRequestException();
            }

            if(!databaseData.TryGetProperty("databaseName", out var databaseName)) {
                throw new BadRequestException();
            }

            connection.AddDatabase(databaseName.GetString());

            return new Response(200);
        }

        public IResponse GetDatabaseInfo(IRequest request)
        {
            var connection = GetConnection(request);
            
            if(request.HttpMethod != "get")
            {
                throw new MethodNotAllowedException();
            }

            var requestedName = request.UrlPlaceholderValues[0];
            var database = connection.Databases.Find(db => db.Name == requestedName);

            if (database == null)
                throw new ResourceNotFoundException("Database does not exist");

            return new Response(JsonSerializer.Serialize(database), 200);
        }

        public IResponse DeleteDatabase(IRequest request)
        {
            var connection = GetConnection(request);

            if (request.HttpMethod != "delete")
            {
                throw new MethodNotAllowedException();
            }

            var databaseToDelete = request.UrlPlaceholderValues[0];

            if(connection.RemoveDatabase(databaseToDelete))
            {
                return new Response(200);
            }

            throw new DatabaseNotFoundException();
        }

        private IConnection GetConnection(IRequest request)
        {
            var connection = _connectionManager.GetConnection(request.AuthToken);
            if(connection == null)
            {
                throw new AuthenticationFailedException();
            }
            return connection;
        }
    }
}
