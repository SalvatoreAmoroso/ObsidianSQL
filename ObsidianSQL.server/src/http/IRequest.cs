﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.http
{
    public interface IRequest
    {
        public Uri Url { get; set; }
        public string HttpMethod { get; set; }
    }
}