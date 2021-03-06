﻿using ObsidianSQL.server.src.http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.http
{
    public class Request : IRequest
    {
        public Uri Url { get; set; }
        public string UserHostAddress { get; set; }
        public string HttpMethod { get; set; }
        public string HttpBodyContent { get; set; }
        public string AuthToken { get; set; }
        public List<string> UrlPlaceholderValues { get; set; }
        public NameValueCollection QueryParameters { get; set; }

        public Request(HttpListenerRequest request)
        {
            Url = request.Url;
            UserHostAddress = request.UserHostAddress;
            HttpMethod = request.HttpMethod.ToLower();
            if(request.InputStream != null)
            {
                HttpBodyContent = GetBodyData(request.InputStream);
                AuthToken = request.Headers.Get("Authorization")?.Replace("Bearer", "").Trim();
                QueryParameters = request.QueryString;
            }
        }

        public Request() { }

        private static string GetBodyData(Stream stream)
        {
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
