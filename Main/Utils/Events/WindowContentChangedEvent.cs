using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    internal enum WindowContentEventType
    {
        Unknown, Exit, MainMenu, GamePreparation, Game
    }

    internal record WindowContentChangedEvent
    {
        public WindowContentEventType Type { get; init; }

        public WindowContentChangedEvent(WindowContentEventType type) =>
            (Type) = (type);
    }
}
