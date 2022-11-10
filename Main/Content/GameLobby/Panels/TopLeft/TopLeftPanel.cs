using Main.Content.Common;
using Main.Content.Common.MapManager;
using Main.Content.Game;
using Main.Content.Game.Terrains;
using Main.Content.GameLobby.Panels.TopLeft;
using Main.Content.Lobby;
using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    public class TopLeftPanel : GameLobbyPanel, IEventHandler<GameLobbyResultPlayersChanged>
    {
        public static readonly Vector2f Position = new Vector2f(Gap, Gap);
        public static readonly Vector2f Size = new Vector2f(0.4f * GameSettings.WindowWidth, 0.4f * GameSettings.WindowWidth);

        private RectangleShape Shape { get; init; }

        private MapPreview _mapPreview;
        private IPlayerManager _playerManager;

        public TopLeftPanel(IGameLobbyContent gameContent, IPlayerManager playerManager) : base(gameContent)
        {
            this._playerManager = playerManager;

            var rectangle = new FloatRect(Position, Size);
            this.View = new TopLeftView(rectangle);

            this.Shape = new RectangleShape(Size);

            this.Shape.Position = Position;
            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 2.0f;

            this._mapPreview = new MapPreview(new Map(), playerManager);
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);

            this._mapPreview.Draw(drawer);
        }

        public override void Handle(MouseEvent e) { }

        public override void Handle(GameLobbyResultMapInfoChanged e)
        {
            this._mapPreview = new MapPreview(e.MapInfo, this._playerManager);
        }

        public void Handle(GameLobbyResultPlayersChanged e)
        {
            this._mapPreview.Handle(e);
        }
    }
}
