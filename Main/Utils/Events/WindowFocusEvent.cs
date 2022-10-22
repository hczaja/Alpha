using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    internal enum WindowFocusEventType
    {
        Unknown, FocusGained, FocusLost
    }

    internal class WindowFocusEvent
    {
        public WindowFocusEventType Type { get; init; }

        public WindowFocusEvent(WindowFocusEventType type) => (Type) = (type);
    }
}
