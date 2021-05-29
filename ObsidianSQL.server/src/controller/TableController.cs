using System.Text.Json;
using ObsidianSQL.library;
using ObsidianSQL.server.db;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.server.src.controller
{
	public class TableController
	{
		private readonly ConnectionManager _connectionManager;

		public TableController(ConnectionManager con)
		{
			_connectionManager = con;
		}
		
		public IResponse GetTableInfo(IRequest request)
		{
			var connection = GetConnection(request);
            
			if(request.HttpMethod != "get")
			{
				throw new MethodNotAllowedException();
			}

			var database = connection.Databases.Find(db => db.Name == request.UrlPlaceholderValues[0]);
			if (database == null)
				throw new ResourceNotFoundException("Database does not exist.");
			
			var requestedTableName = request.UrlPlaceholderValues[0];
			var table = database.Tables.Find(t => t.Name == requestedTableName);

			if (table == null)
				throw new ResourceNotFoundException("Table does not exist.");

			return new Response(JsonSerializer.Serialize(table),200);
		}
		
		private IConnection GetConnection(IRequest request)
		{
			var connection = _connectionManager.GetConnection(request.AuthToken);
			if(connection == null)
			{
				throw new AuthentificationFailedException();
			}
			return connection;
		}
	}
}