using Main.Content.Common.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    public class GameLobbyResult
    {
        public Map MapInfo { get; set; }

        public GameLobbyResult()
        {
            this.MapInfo = new Map();
        }
    }
}
