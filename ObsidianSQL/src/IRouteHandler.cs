﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src
{
    interface IRouteHandler
    {
        public Response GetResponse();
    }
}