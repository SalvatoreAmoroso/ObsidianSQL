using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.http
{
    class HttpListener : IHttpListener
    {
        private System.Net.HttpListener _httpListener;
        private Dictionary<IRequest, HttpListenerContext> _contexts;

        public HttpListener()
        {
            _httpListener = new();
            _contexts = new();
        }

        public void AddPrefix(string prefix)
        {
            _httpListener.Prefixes.Add(prefix);
        }

        public async Task<IRequest> WaitForRequest()
        {
            var context = await _httpListener.GetContextAsync();
            IRequest request = new Request(context.Request);
            _contexts[request] = context;
            return request;
        }

        public void WriteResponse(IRequest request, IResponse response)
        {
            var context = _contexts[request];
            var res = context.Response;
            res.StatusCode = response.HttpStatusCode;

            var responseBuffer = Array.Empty<byte>();
            if(response != null)
            {
                responseBuffer = Encoding.UTF8.GetBytes(response.Content);
                if(CheckForJSON(response.Content))
                {
                    res.Headers.Set("Content-Type", "application/json");
                }
            }

            using var output = res.OutputStream;
            output.Write(responseBuffer);
        }

        private bool CheckForJSON(string s)
        {
            try
            {
                JsonDocument.Parse(s);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public void Start()
        {
            _httpListener.Start();
        }

        public void Close()
        {
            _httpListener.Close();
        }
    }
}
