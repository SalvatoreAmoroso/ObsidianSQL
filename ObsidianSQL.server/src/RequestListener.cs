using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using ObsidianSQL.server.src.http;
using ObsidianSQL.server.src;
using ObsidianSQL.library;
using ObsidianSQL.server.src.exceptions;

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
                var request = new Request(context.Request);

                //Manage Request
                var response = context.Response;

                try
                {
                    var responseDTO = _router.ManageRequest(request);
                    var responseBuffer = Encoding.UTF8.GetBytes(responseDTO.Content);


                    //Write Response
                    using var output = response.OutputStream;
                    output.Write(responseBuffer);
                }
                catch (AuthentificationFailedException ex)
                {
                    Console.WriteLine(ex.Message);
                    response.StatusCode = 401;
                }
                catch (RouteNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    response.StatusCode = 404;
                }
            }
        }

        public void Stop()
        {
            _runServer = false;
        }

        public void Dispose()
        {
            _httpListener.Close();
        }
    }
}