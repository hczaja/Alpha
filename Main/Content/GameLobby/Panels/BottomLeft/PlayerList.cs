using Main.Content.Common.MapManager;
using Main.Content.Game;
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
        private PlayersListEntry? _currentEntry = null;

        private readonly IGameLobbyContent _gameLobbyContent;

        public PlayersList(IGameLobbyContent gameLobbyContent)
        {
            this._gameLobbyContent = gameLobbyContent;

            var map = gameLobbyContent.GetMapInfo();
            int playersAmount = map.Players + 1;

            this._entries = new PlayersListEntry[playersAmount];
            this._entries[0] = this.GetHeader();
            this.FillPlayerList(playersAmount);
        }

        private void FillPlayerList(int players)
        {
            var allFactions = Faction.GetAllFactionTypes();
            for (int index = 1; index < players; index++)
            {
                var info = new PlayerInfo(index.ToString(), "<Empty>", allFactions[index - 1].ToString());
                this._entries[index] = new PlayersListEntry(index, info);
            }
        }

        private PlayersListEntry GetHeader() => new PlayersListEntry(0, new PlayerInfo("#", "Name", "Faction"));

        public void Draw(RenderTarget drawer)
        {
            foreach (var entry in this._entries)
            {
                entry.Draw(drawer);
            }
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed && e.Button == Mouse.Button.Left)
            {
                foreach (var entry in this._entries.Skip(1))
                {
                    if (MouseEvent.IsMouseEventRaisedIn(entry.Shape.GetGlobalBounds(), e))
                    {
                        this._currentEntry?.Unselect();

                        this._currentEntry = entry;
                        this._currentEntry.Select();

                        _gameLobbyContent.Handle(new GameLobbyResultPlayersInfoChanged(this._currentEntry.PlayerInfo));
                    }
                }
            }
        }

        public void Handle(GameLobbyResultMapInfoChanged e)
        {
            int playersAmount = e.MapInfo.Players + 1;

            this._entries = new PlayersListEntry[playersAmount];
            this._entries[0] = this.GetHeader();
            this.FillPlayerList(playersAmount);

            this._currentEntry?.Unselect();

            this._currentEntry = this._entries.ElementAt(1);
            this._currentEntry.Select();

            _gameLobbyContent.Handle(new GameLobbyResultPlayersInfoChanged(this._currentEntry.PlayerInfo));
        }
    }
}
