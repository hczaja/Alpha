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
    public class TopLeftPanel : GameLobbyPanel
    {
        public static readonly Vector2f Position = new Vector2f(Gap, Gap);
        public static readonly Vector2f Size = new Vector2f(0.4f * GameSettings.WindowWidth, 0.4f * GameSettings.WindowWidth);

        private RectangleShape Shape { get; init; }

        private MapPreview _mapPreview;
        private MapInfo _mapInfo;

        public TopLeftPanel(IGameLobbyContent gameContent) : base(gameContent)
        {
            var rectangle = new FloatRect(Position, Size);
            this.View = new TopLeftView(rectangle);

            this.Shape = new RectangleShape(Size);

            this.Shape.Position = Position;
            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 2.0f;

            this._mapInfo = this._gameContent.GetMapInfo();
            this._mapPreview = new MapPreview(this._mapInfo);
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);

            this._mapPreview.Draw(drawer);
        }

        public override void Handle(MouseEvent e) { }
    }
}
