using Main.Content.Common;
using Main.Content.Game.Factions;
using Main.Content.Lobby;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels.BottomLeft
{
    public class PlayersList : 
        IDrawable, 
        IEventHandler<MouseEvent>,
        IEventHandler<GameLobbyResultMapInfoChanged>
    {
        private PlayersListEntry[] _entries;

        private readonly IGameLobbyContent _gameLobbyContent;
        private readonly IPlayerManager _playerManager;

        public PlayersList(IGameLobbyContent gameLobbyContent, IPlayerManager playerManager)
        {
            this._gameLobbyContent = gameLobbyContent;
            this._playerManager = playerManager;

            var map = this._gameLobbyContent.GetMapInfo();
            int playersAmount = map.Players;

            this._entries = new PlayersListEntry[playersAmount + 1];
            this._entries[0] = this.GetHeader();

            this.FillPlayerList(playersAmount);
        }

        private void FillPlayerList(int players)
        {
            for (int index = 0; index < players; index++)
            {
                this._entries[index + 1] = new PlayersListEntry(index + 1, this._playerManager.Players[index], this._playerManager);
            }
        }

        private PlayersListEntry GetHeader() => new PlayersListEntry(0, new PlayerInfo("#", "Name", "Type", "Faction", "Team"), this._playerManager);

        public void Draw(RenderTarget drawer)
        {
            foreach (var entry in this._entries)
            {
                entry.Draw(drawer);
            }
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed)
            {
                foreach (var entry in this._entries.Skip(1))
                {
                    entry.Handle(e);
                }
            }
        }

        public void Handle(GameLobbyResultMapInfoChanged e)
        {
            int playersAmount = e.MapInfo.Players;

            this._entries = new PlayersListEntry[playersAmount + 1];
            this._entries[0] = this.GetHeader();

            this.FillPlayerList(playersAmount);
        }
    }
}
