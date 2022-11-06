using Main.Content.Common.MapManager;
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

namespace Main.Content.GameLobby.Panels.TopRight
{
    public class MapList : IDrawable, IEventHandler<MouseEvent>
    {
        private MapListEntry[] _entries { get; init; }
        private MapListEntry? _currentEntry = null;

        private readonly IGameLobbyContent _gameLobbyContent;

        public MapList(IGameLobbyContent gameLobbyContent)
        {
            _gameLobbyContent = gameLobbyContent;

            var mapRegistry = MapManager.RegistryInstance;
            var maps = mapRegistry.Maps;

            this._entries = new MapListEntry[maps.Count + 1];
            this._entries[0] = this.GetHeader();

            for (int index = 0; index < maps.Count; index++)
            {
                var map = maps[index];
                this._entries[index + 1] = new MapListEntry(index + 1, map, new MapInfo((index + 1).ToString(), map.Name, map.GridSize.ToString(), map.Players.ToString()));
            }

            this._currentEntry = this._entries.ElementAt(1);
            this._currentEntry.Select();

            _gameLobbyContent.Handle(new GameLobbyResultMapInfoChanged(this._currentEntry.Map));
        }

        private MapListEntry GetHeader() => new MapListEntry(0, null!, new MapInfo("#", "Name", "Size", "Players"));

        public void Draw(RenderTarget drawer)
        {
            foreach (var entry in this._entries)
            {
                if (entry == this._currentEntry)
                {
                    continue;
                }

                entry.Draw(drawer);
            }

            this._currentEntry?.Draw(drawer);
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

                        _gameLobbyContent.Handle(new GameLobbyResultMapInfoChanged(this._currentEntry.Map));
                    }
                }
            }
        }
    }
}
