using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.http
{
    public interface IHttpListener
    {
        public void AddPrefix(string prefix);
        public Task<IRequest> WaitForRequest();
        public void WriteResponse(IRequest request, IResponse response);
        public void Start();
        public void Close();

    }
}
