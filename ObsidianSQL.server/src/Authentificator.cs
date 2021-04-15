using ObsidianSQL.library;
using ObsidianSQL.server.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsidianSQL.library.mockup;
using ObsidianSQL.server.src.exceptions;

namespace ObsidianSQL.server.src
{
    class Authentificator
    {
        readonly object DBConnector;

        public Authentificator()
        {

        }

        public IConnection Authentificate(IRequest request)
        {
            //Request Body Might Contain:
            // 1. Username + Password or 2. JWT

            //Ask DBConnector
            bool connectionSucceed = true;

            if (!connectionSucceed) throw new AuthentificationFailedException();

            Connection con = new();

            return con;
        }

    }
}
