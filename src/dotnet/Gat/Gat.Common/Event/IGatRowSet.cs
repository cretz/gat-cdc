using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Event
{
    public interface IGatRowSet
    {
        IEnumerable<IGatColumn> Columns { get; }
        IGatColumn GetColumn(int ordinal);
        IEnumerable<IGatRow> Rows { get; }
        IGatRow GetRow(int ordinal);
    }
}
