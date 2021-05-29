﻿using ObsidianSQL.library;
using ObsidianSQL.server.src.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ObsidianSQL.server.db;
using System.Dynamic;
using ObsidianSQL.server.src.exceptions;
using Serilog;

namespace ObsidianSQL.server.src.controller
{
    public class LoginController
    {
        private readonly ConnectionManager _connectionManager;

        public LoginController(ConnectionManager con)
        {
            _connectionManager = con;
        }

        public IResponse Login(IRequest request)
        {
            if(request.HttpMethod.ToLower() != "post")
            {
                throw new MethodNotAllowedException();
            }

            dynamic tokenResponse = new ExpandoObject();
            tokenResponse.token = _connectionManager.CreateConnection(request);

            var json = JsonSerializer.Serialize(tokenResponse);
            return new Response(json, 200);
        }
    }
}
