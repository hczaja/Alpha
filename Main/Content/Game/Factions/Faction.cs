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

        public Color GetFactionColor() => FactionToColor(this.Type);

        public static Color FactionToColor(FactionType type) => type switch
        {
            FactionType.Undeads => new Color(124, 102, 169),
            FactionType.Humans => new Color(74, 120, 194),
            FactionType.Dwarves => new Color(103, 185, 191),
            _ => Color.White
        };

        public static FactionType[] GetAllFactionTypes() => new FactionType[] { FactionType.Undeads, FactionType.Humans, FactionType.Dwarves };

        public Faction(FactionType type) => (Type) = (type);
    }
}
