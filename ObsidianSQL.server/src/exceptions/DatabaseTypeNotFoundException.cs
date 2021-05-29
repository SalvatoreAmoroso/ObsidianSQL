using System;

namespace ObsidianSQL.server.src.exceptions
{
    class DatabaseTypeNotFoundException : Exception
    {
        public DatabaseTypeNotFoundException()
        {
        }

        public DatabaseTypeNotFoundException(string message)
            : base(message)
        {
        }

        public DatabaseTypeNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}