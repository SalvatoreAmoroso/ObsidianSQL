using ObsidianSQL.server.src.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.http
{
    public class Response : IResponse
    {
        public string Content { get; set; }
        public Response(string content)
        {
            Content = content;
        }
    }
}
