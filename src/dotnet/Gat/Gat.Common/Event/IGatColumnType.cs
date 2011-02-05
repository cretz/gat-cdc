using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Event
{
    public interface IGatColumnType
    {
        GatDataType Type { get; }
        string TypeName { get; }
        int? Precision { get; }
        int? Scale { get; }
    }
}
