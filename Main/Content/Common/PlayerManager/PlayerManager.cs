using Main.Utils.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Common
{
    public interface IPlayerManager
        : IEventHandler<GameLobbyResultMapInfoChanged>
    {
        PlayerInfo[] Players { get; }

        public bool IsAtLeastOneHumanPlayer() => this.Players.Any(p => p.Type == PlayerType.Human.ToString());
        public bool AreAtLeastTwoDifferentTeams() => this.Players.Where(p => p.Type != PlayerType.Empty.ToString()).GroupBy(p => p.Team).Count() > 1;
        public bool AreUniqueFactions() => this.Players.Where(p => p.Type != PlayerType.Empty.ToString()).GroupBy(p => p.Faction).All(factions => factions.Count() == 1);
    }

    public class PlayerManager : IPlayerManager
    {
        public PlayerInfo[] Players { get; private set; }

        public PlayerManager(int amount)
        {
            this.Players = new PlayerInfo[amount];
            for (int index = 0; index < amount; index++)
            {
                this.Players[index] = new PlayerInfo(index.ToString(), "", "Empty", "Undeads", (index + 1).ToString());
            }
        }

        public void Handle(GameLobbyResultMapInfoChanged e)
        {
            var amountOfPlayers = e.MapInfo.Players;
            this.Players = new PlayerInfo[amountOfPlayers];

            for (int index = 0; index < amountOfPlayers; index++)
            {
                this.Players[index] = new PlayerInfo(index.ToString(), "", "Empty", "Undeads", (index + 1).ToString());
            }
        }
    }
}
