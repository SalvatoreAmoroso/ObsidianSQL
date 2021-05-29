using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.library;
using ObsidianSQL.server.db;
using ObsidianSQL.server.http;

namespace ObsidianSQL.server.src.controller
{
    public interface IController
    {
        public IResponse GetResponse(IRequest request);
    }
}