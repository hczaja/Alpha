using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public enum WindowContentEventType
    {
        Unknown, Exit, MainMenu, GameLobby, Game
    }

    public record WindowContentChangedEvent
    {
        public WindowContentEventType Type { get; init; }

        public WindowContentChangedEvent(WindowContentEventType type) =>
            (Type) = (type);
    }
}
