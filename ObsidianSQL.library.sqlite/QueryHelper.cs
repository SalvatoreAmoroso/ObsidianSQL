using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.sqlite
{
    class QueryHelper
    {
        public static SQLiteDataReader ExecuteDatabaseQuery(SQLiteConnection connection, string command)
        {
            var tableNamesCommand = connection.Connection.CreateCommand();
            tableNamesCommand.CommandText = command;
            return tableNamesCommand.ExecuteReader();
        }

        public static int ExecuteDatabaseCommand(SQLiteConnection connection, string command)
        {
            var tableNamesCommand = connection.Connection.CreateCommand();
            tableNamesCommand.CommandText = command;
            return tableNamesCommand.ExecuteNonQuery();
        }
    }
}
