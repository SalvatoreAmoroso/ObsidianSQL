using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using ObsidianSQL.library;
using ObsidianSQL.server.src.exceptions;
using Serilog;
using ObsidianSQL.server.src.http;
using ObsidianSQL.server.src;

namespace ObsidianSQL.server
{
    public class RequestListener : IDisposable
    {
        private readonly IHttpListener _httpListener;

        private readonly IRouter _router;

        private bool _runServer;

        public RequestListener(IHttpListener httpListener, string[] prefixes, IRouter router)
        {
            _router = router;

            _httpListener = httpListener;

            //Add prefixes to HttpListener
            prefixes.ToList().ForEach(prefix => _httpListener.AddPrefix(prefix));

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
                var request = await _httpListener.WaitForRequest();
                Log.Debug($"Received a request to '{request.Url}' from {request.UserHostAddress}");

                IResponse response = new Response("", 0);

                try
                {
                    response = _router.ManageRequest(request);

                    if (response.HttpStatusCode == 0)
                    {
                        throw new InvalidDataException("Invalid http status code.");
                    }

                    response.HttpStatusCode = response.HttpStatusCode;
                    Log.Debug($"Request successfully processed");
                }
                catch (BadRequestException ex)
                {
                    Log.Error(ex.Message);
                    response.HttpStatusCode = 400;
                }
                catch (AuthenticationFailedException ex)
                {
                    Log.Error(ex.Message);
                    response.HttpStatusCode = 401;
                }
                catch (UnauthorizedAccessException ex)
                {
                    Log.Error(ex.Message);
                    response.HttpStatusCode = 403;
                }
                catch (DatabaseNotFoundException ex)
                {
                    Log.Error(ex.Message); ;
                    response.HttpStatusCode = 404;
                }
                catch (RouteNotFoundException ex)
                {
                    Log.Error(ex.Message); ;
                    response.HttpStatusCode = 404;
                }
                catch (ResourceNotFoundException ex)
                {
                    Log.Error(ex.Message); ;
                    response.HttpStatusCode = 404;
                }
                catch (MethodNotAllowedException ex)
                {
                    Log.Error(ex.Message); ;
                    response.HttpStatusCode = 405;
                }
                catch (DatabaseTypeNotFoundException ex)
                {
                    Log.Error(ex.Message); ;
                    response.HttpStatusCode = 422;
                }

                _httpListener.WriteResponse(request, response);
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