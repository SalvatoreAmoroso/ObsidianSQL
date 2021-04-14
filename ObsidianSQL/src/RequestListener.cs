using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ObsidianSQL.server.src
{
    class RequestListener : IDisposable
    {
        private readonly HttpListener _httpListener;

        private readonly Router _router;

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
            //Wait for a request
            var context = await _httpListener.GetContextAsync();
            var request = context.Request;

            //Manage Request
            //TODO: Manage Exception Handling
            //var responseDTO = _router.Evaluate(new Request(request));

            //Create Response
            var response = context.Response;

            var responseBuffer = Encoding.UTF8.GetBytes("Test"/*responseDTO.ResponseText*/);

            //Write Response
            using var output = response.OutputStream;
            output.Write(responseBuffer);
        }

        public void Dispose()
        {
            _httpListener.Close();
        }
    }
}