using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.exceptions
{
    class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException()
        {
        }

        public MethodNotAllowedException(string message)
            : base(message)
        {
        }

        public MethodNotAllowedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
