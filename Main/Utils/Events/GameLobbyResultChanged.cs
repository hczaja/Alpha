using Main.Content.Common.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class GameLobbyResultChanged
    {
        public MapInfo MapInfo { get; private init; }

        public GameLobbyResultChanged(MapInfo mapInfo)
            => (MapInfo) = (mapInfo);
    }
}
