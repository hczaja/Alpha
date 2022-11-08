using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Factions
{
    public enum FactionType
    {
        Undeads, Humans, Dwarves
    }

    public static class FactionTypeExtensions
    {
        public static Color FactionToColor(FactionType type) => type switch
        {
            FactionType.Undeads => new Color(124, 102, 169),
            FactionType.Humans => new Color(74, 120, 194),
            FactionType.Dwarves => new Color(103, 185, 191),
            _ => Color.White
        };

        public static FactionType[] GetFactionTypes() => new FactionType[] { FactionType.Undeads, FactionType.Humans, FactionType.Dwarves };

        public static FactionType GetNextFactionType(FactionType type)
        {
            var factionTypes = GetFactionTypes().ToList();
            int index = factionTypes.IndexOf(type);

            int nextIndex = index + 1;

            FactionType result;
            try
            {
                result = factionTypes[nextIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                result = factionTypes[0];
            }

            return result;
        }
    }
}
