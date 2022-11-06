using Main.Content.Common.MapManager;
using Main.Content.GameLobby.Panels.TopRight;
using Main.Content.Lobby;
using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    public class TopRightPanel : GameLobbyPanel
    {
        public static readonly Vector2f Position = new Vector2f(TopLeftPanel.Position.X + TopLeftPanel.Size.X + Gap, Gap);
        public static readonly Vector2f Size = new Vector2f(GameSettings.WindowWidth - Gap - TopLeftPanel.Size.X - TopLeftPanel.Position.X - Gap, 0.4f * GameSettings.WindowWidth);

        private RectangleShape Shape { get; init; }
        
        private MapList _mapList;

        public TopRightPanel(IGameLobbyContent gameContent) : base(gameContent)
        {
            var rectangle = new FloatRect(Position, Size);
            this.View = new TopRightView(rectangle);

            this.Shape = new RectangleShape(Size);

            this.Shape.Position = Position;
            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 2.0f;

            this._mapList = new MapList();
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);

            this._mapList.Draw(drawer);
        }

        public override void Handle(MouseEvent e) 
        {
            this._mapList.Handle(e);
        }
    }
}
