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

        public Color GetFactionColor() => this.Type switch
        {
            FactionType.Undeads => new Color(124, 102, 169),
            _ => Color.White
        };

        public Faction(FactionType type) => (Type) = (type);
    }
}
