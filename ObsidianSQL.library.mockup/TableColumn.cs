using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.mockup
{
    public class TableColumn : ITableColumn
    {
        public string Name { get; set; }
        public string Datatype { get; set; }

        public TableColumn(string name, string datatype)
        {
            Name = name;
            Datatype = datatype;
        }
    }
}
