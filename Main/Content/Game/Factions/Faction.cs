using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Factions
{
    internal class Faction
    {
        public FactionType Type { get; init; }

        public Faction(FactionType type) => (Type) = (type);
    }
}
