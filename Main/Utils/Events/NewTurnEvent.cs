using Main.Content.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public record NewTurnEvent
    {
        public int TurnNumber { get; }
        public Player PlayerInfo { get; }

        public NewTurnEvent(int turnNumber, Player info) => (TurnNumber, PlayerInfo) = (turnNumber, info);
    }
}
