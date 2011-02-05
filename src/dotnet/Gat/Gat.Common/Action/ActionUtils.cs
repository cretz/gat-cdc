using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common.Event;

namespace Gat.Common.Action
{
    public static class ActionUtils
    {
        private delegate IGatEventResponse AsyncRun(IGatEvent evt, bool forceClone);

        public static IGatEventResponse RunSync(IAction action, IGatEvent evt)
        {
            return action.PrepareAction(evt, false).Invoke();
        }

        public static void RunAsync(IAction action, IGatEvent evt)
        {
            action.PrepareAction(evt, true).BeginInvoke(null, null);
        }
    }
}
