using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library
{
    public interface ITableRow
    {
        public IDataField<T> GetDataField<T>(string column);
        public List<IDataField<object>> DataFields { get; }
    }
}
