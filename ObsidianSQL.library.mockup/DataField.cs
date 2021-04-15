using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianSQL.library.mockup
{
    public class DataField<T> : IDataField<T>
    {
        public string ColumnName { get; set; }
        public T Value { get; set; }

        public DataField(string name, T value)
        {
            ColumnName = name;
            Value = value;
        }
    }
}
