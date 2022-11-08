using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Factions
{
    public class Faction
    {
        public FactionType Type { get; init; }

        public Color GetFactionColor() => FactionTypeExtensions.FactionToColor(this.Type);

        public Faction(FactionType type) => (Type) = (type);
    }
}
