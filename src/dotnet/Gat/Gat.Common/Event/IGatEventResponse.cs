using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Event
{
    public interface IGatEventResponse
    {
        bool? Rollback { get; }
        string ExecuteInstead { get; }
        IGatRowSet ReturnInstead { get; }
        IEnumerable<string> Messages { get; }
    }
}
