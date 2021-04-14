using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src
{
    class Request
    {
        public Uri Url { get; set; }

        public string HttpMethod { get; set; }

        public Request(HttpListenerRequest request)
        {
            Url = request.Url;
            HttpMethod = request.HttpMethod;
        }
    }
}