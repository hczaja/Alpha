using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Common.MapManager
{
    public class PlayerInfo
    {
        public string Index { get; private init; }
        public string Name { get; private init; }
        public string Faction { get; private init; }

        public PlayerInfo(string index, string name, string faction)
            => (Index, Name, Faction) = (index, name, faction);
    }
}
