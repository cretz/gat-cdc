using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Event
{
    public interface IGatColumn
    {
        int Ordinal { get; }
        string Name { get; }
        IGatColumnType Type { get; }
    }
}
