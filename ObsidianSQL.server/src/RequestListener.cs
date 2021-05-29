using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;
using ObsidianSQL.library;
using ObsidianSQL.server.src.exceptions;
using Serilog;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.server
{
    class RequestListener : IDisposable
    {
        private readonly HttpListener _httpListener;

        private readonly Router _router;

        private bool _runServer;

        public RequestListener(string[] prefixes, Router router)
        {
            _router = router;

            _httpListener = new HttpListener();

            //Add prefixes to HttpListener
            prefixes.ToList().ForEach(prefix => _httpListener.Prefixes.Add(prefix));

            //Start Listener
            _httpListener.Start();
            Log.Information("HttpListener started");
        }

        /// <summary>
        /// Receive Requests und send response asynchronously (does not block the thread)
        /// </summary>
        public async void HandleRequests()
        {
            _runServer = true;

            while (_runServer)
            {
                //Wait for a request
                var context = await _httpListener.GetContextAsync();
                Log.Debug($"Received a request to '{context.Request.Url}' from {context.Request.UserHostAddress}");

                var request = new Request(context.Request);

                //Manage Request
                var response = context.Response;
                
                IResponse responseDTO = null;

                try
                {
                    responseDTO = _router.ManageRequest(request);

                    if(responseDTO.HttpStatusCode == 0)
                    {
                        throw new InvalidDataException("Invalid http status code.");
                    }

                    response.StatusCode = responseDTO.HttpStatusCode;
                    Log.Debug($"Request successfully processed");
                }
                catch (BadRequestException ex)
                {
                    Log.Error(ex.Message);
                    response.StatusCode = 400;
                }
                catch (AuthentificationFailedException ex)
                {
                    Log.Error(ex.Message);
                    response.StatusCode = 401;
                }
                catch (RouteNotFoundException ex)
                {
                    Log.Error(ex.Message); ;
                    response.StatusCode = 404;
                }
                catch (ResourceNotFoundException ex)
                {
                    Log.Error(ex.Message); ;
                    response.StatusCode = 404;
                }
                catch (MethodNotAllowedException ex)
                {
                    Log.Error(ex.Message); ;
                    response.StatusCode = 405;
                }

                try
                {
                    JsonDocument.Parse(responseDTO.Content);
                    response.Headers.Set("Content-Type", "application/json");
                }
                catch (Exception) { }

                var responseBuffer = responseDTO == null ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(responseDTO.Content);

                using var output = response.OutputStream;
                output.Write(responseBuffer);
                Log.Debug("Response sent");
            }
        }

        public void Stop()
        {
            Log.Information("Stop HttpListener");
            _runServer = false;
        }

        public void Dispose()
        {
            _httpListener.Close();
        }
    }
}