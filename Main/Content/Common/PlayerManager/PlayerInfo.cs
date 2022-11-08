using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Common
{
    public class PlayerInfo
    {
        public string Index { get; private init; }

        public string Type { get; set; }
        public string Team { get; set; }

        public string Name { get; private init; }
        public string Faction { get; set; }

        public PlayerInfo(string index, string name, string type, string faction, string team)
            => (Index, Name, Type, Faction, Team) = (index, name, type, faction, team);
    }
}
