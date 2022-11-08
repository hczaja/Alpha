using Main.Content.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class GameLobbyResultPlayersInfoChanged
    {
        public PlayerInfo PlayerInfo { get; private init; }

        public GameLobbyResultPlayersInfoChanged(PlayerInfo playerInfo)
            => (PlayerInfo) = (playerInfo);
    }
}
