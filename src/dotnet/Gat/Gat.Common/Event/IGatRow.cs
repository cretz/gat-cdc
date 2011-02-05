using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Event
{
    public interface IGatRow
    {
        IEnumerable<IGatValue> Values { get; }
        IGatValue GetValue(int ordinal);
    }
}
