using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.mockup
{
    public class Table : ITable
    {
        public string Name { get; set; }
        public ITableColumn[] Columns { get; set; }
        public ITableRow[] GetData(int start, int end)
        {
            return new TableRow[] { new TableRow(), new TableRow(), new TableRow(), new TableRow() };
        }

        public Table(string name)
        {
            Name = name;
            Columns = new TableColumn[] { new TableColumn("name","string"), new TableColumn("age","int"), new TableColumn("height","int") };
        }
    }
}
