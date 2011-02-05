using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Event
{
    public interface IGatEvent
    {
        string Id { get; }
        GatEventType Type { get; }
        string Catalog { get; }
        string Schema { get; }
        string Table { get; }
        string Sql { get; }
        IGatRowSet RowSet { get; }
    }
}
