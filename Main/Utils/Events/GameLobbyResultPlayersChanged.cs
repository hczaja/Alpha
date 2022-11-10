using Main.Content.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class GameLobbyResultPlayersChanged
    {
        public IPlayerManager PlayerManager { get; private init; }

        public GameLobbyResultPlayersChanged(IPlayerManager playerManager)
            => (PlayerManager) = (playerManager);
    }
}
