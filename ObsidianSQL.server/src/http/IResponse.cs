using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.http
{
    public interface IResponse
    {
        public string Content { get; set; }
        
        public int HttpStatusCode { get; set; }
    }
}
