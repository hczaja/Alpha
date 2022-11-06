using Main.Content.Common.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class GameLobbyResultMapInfoChanged
    {
        public Map MapInfo { get; private init; }

        public GameLobbyResultMapInfoChanged(Map mapInfo)
            => (MapInfo) = (mapInfo);
    }
}
