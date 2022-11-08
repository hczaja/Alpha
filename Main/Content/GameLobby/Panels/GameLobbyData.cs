using Main.Content.Common;
using Main.Content.Common.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    public class GameLobbyData
    {
        public Map MapInfo { get; set; }
        public IPlayerManager PlayerManager { get; set; }

        public GameLobbyData(IPlayerManager playerManager)
        {
            this.MapInfo = new Map();
            this.PlayerManager = playerManager;
        }
    }
}
