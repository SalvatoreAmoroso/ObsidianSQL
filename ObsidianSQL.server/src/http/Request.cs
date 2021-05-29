using ObsidianSQL.server.http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.http
{
    class Request : IRequest
    {
        public Uri Url { get; set; }
        public string HttpMethod { get; set; }
        public string HttpBodyContent { get; set; }
        public string AuthToken { get; set; }


        public Request(HttpListenerRequest request)
        {
            Url = request.Url;
            HttpMethod = request.HttpMethod;
            if(request.InputStream != null)
            {
                HttpBodyContent = GetBodyData(request.InputStream);
                AuthToken = request.Headers.Get("Authorization")?.Replace("Bearer ", "");
            }
        }

        private static string GetBodyData(Stream stream)
        {
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
