using System;
using System.Linq;
using System.Text.Json;
using ObsidianSQL.library;
using ObsidianSQL.server.src.db;
using ObsidianSQL.server.src.exceptions;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.server.src.controller
{
	public class TableController : AbstractController
	{
		public TableController(IConnectionManager con)
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
			
			var requestedTableName = request.UrlPlaceholderValues[1];
			var table = database.Tables.Find(t => t.Name == requestedTableName);

			if (table == null)
				throw new ResourceNotFoundException("Table does not exist.");

			return new Response(JsonSerializer.Serialize(table),200);
		}
		
		public IResponse GetTableData(IRequest request)
		{
			var connection = GetConnection(request);
            
			if(request.HttpMethod != "get")
			{
				throw new MethodNotAllowedException();
			}

			var database = connection.Databases.Find(db => db.Name == request.UrlPlaceholderValues[0]);
			if (database == null)
				throw new ResourceNotFoundException("Database does not exist.");
			
			var requestedTableName = request.UrlPlaceholderValues[1];
			var table = database.Tables.Find(t => t.Name == requestedTableName);

			if (table == null)
				throw new ResourceNotFoundException("Table does not exist.");

			string startString = request.QueryParameters.Get("start");
			int start = 0;
			if (startString != null)
				start = Int32.Parse(startString);

			string endString = request.QueryParameters.Get("end");
			int end = start + 30;
			if (endString != null)
				end = Int32.Parse(endString);

			var data = table.GetData(start, end).ToList().FindAll(row => row != null);
			Console.WriteLine(data[0]);
			return new Response(JsonSerializer.Serialize(data),200);
		}
	}
}