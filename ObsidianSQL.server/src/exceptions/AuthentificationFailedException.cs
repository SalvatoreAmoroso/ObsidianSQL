using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.server.src.exceptions
{
    class AuthentificationFailedException : Exception
    {
        public AuthentificationFailedException()
        {
        }

        public AuthentificationFailedException(string message)
            : base(message)
        {
        }

        public AuthentificationFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
