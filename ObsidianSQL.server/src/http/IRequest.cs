using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.http
{
    public interface IRequest
    {
        public Uri Url { get; set; }
        public string UserHostAddress { get; set; }
        public string HttpMethod { get; set; }
        public string HttpBodyContent { get; set; }
        public string AuthToken { get; set; }
        public List<string> UrlPlaceholderValues { get; set; }
        public NameValueCollection QueryParameters { get; set; }
    }
}
