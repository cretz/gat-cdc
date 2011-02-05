using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Event
{
    public interface IGatValue
    {
        int Ordinal { get; }
        T GetValue<T>();
    }
}
