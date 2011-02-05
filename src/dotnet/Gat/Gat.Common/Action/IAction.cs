using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common.Event;

namespace Gat.Common.Action
{
    public interface IAction : IDisposable
    {
        RunnerDelegate PrepareAction(IGatEvent evt, bool forceClone);
    }

    public delegate IGatEventResponse RunnerDelegate();
}
