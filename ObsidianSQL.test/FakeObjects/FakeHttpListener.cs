using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.src.http;

namespace ObsidianSQL.test.FakeObjects
{
    class FakeHttpListener : IHttpListener
    {
        private IRequest _request;
        private IResponse _response;

        public bool IsClosed { get; set; } = false;
        public bool IsStarted { get; set; } = false;
        public List<string> Prefixes { get; set; } = new();

        public void AddPrefix(string prefix)
        {
            Prefixes.Add(prefix);
        }

        public void Close()
        {
            IsClosed = true;
        }

        public void Start()
        {
            IsStarted = true;
        }

        public void SetRequest(IRequest request)
        {
            _request = request;
        }

        public Task<IRequest> WaitForRequest()
        {
            return new Task<IRequest>(() => _request);
        }

        public IResponse GetResponse()
        {
            return _response;
        }

        public void WriteResponse(IRequest request, IResponse response)
        {
            _response = response;
        }
    }
}
