﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.server.http;

namespace ObsidianSQL.server.controller
{
    public interface IController
    {
        public IResponse GetResponse(IRequest request);
    }
}