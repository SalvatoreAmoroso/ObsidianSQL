using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.sqlite
{
    public class QueryHelper
    {
        public virtual SQLiteDataReader ExecuteDatabaseQuery(SQLiteConnection connection, string command)
        {
            var tableNamesCommand = connection.Connection.CreateCommand();
            tableNamesCommand.CommandText = command;
            return tableNamesCommand.ExecuteReader();
        }

        public virtual int ExecuteDatabaseCommand(SQLiteConnection connection, string command)
        {
            var tableNamesCommand = connection.Connection.CreateCommand();
            tableNamesCommand.CommandText = command;
            return tableNamesCommand.ExecuteNonQuery();
        }
    }
}
