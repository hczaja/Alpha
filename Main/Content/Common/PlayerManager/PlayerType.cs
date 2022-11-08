using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Common
{
    public enum PlayerType
    {
        Empty, Human, AI
    }

    public static class PlayerTypeExtensions
    {
        public static IEnumerable<PlayerType> GetPlayerTypes() => new List<PlayerType>() { PlayerType.Empty, PlayerType.AI, PlayerType.Human };
        public static PlayerType GetNextPlayerType(PlayerType type)
        {
            var playerTypes = GetPlayerTypes().ToList();
            int index = playerTypes.IndexOf(type);

            int nextIndex = index + 1;

            PlayerType result;
            try
            {
                result = playerTypes[nextIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                result = playerTypes[0];
            }

            return result;
        }
    }
}
