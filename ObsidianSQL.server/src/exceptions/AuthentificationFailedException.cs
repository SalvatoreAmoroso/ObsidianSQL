using System;

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