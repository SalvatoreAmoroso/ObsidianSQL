using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src
{
    class Response
    {
        public readonly string ResponseText;
        public Response(string responseText)
        {
            ResponseText = responseText;
        }
    }
}
