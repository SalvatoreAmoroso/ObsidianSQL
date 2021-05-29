using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.mockup
{
    public class TableRow : ITableRow
    {
        public IDataField<T> GetDataField<T>(string column)
        {
            throw new NotImplementedException();
        }

        public List<IDataField<object>> DataFields { get; } = new();
    }
}
