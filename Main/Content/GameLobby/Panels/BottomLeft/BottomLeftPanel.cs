using Main.Content.GameLobby.Panels.BottomLeft;
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
    public class BottomLeftPanel : GameLobbyPanel
    {
        public static readonly Vector2f Position = new Vector2f(Gap, TopLeftPanel.Position.Y + TopLeftPanel.Size.Y + Gap);
        public static readonly Vector2f Size = new Vector2f(0.6f * GameSettings.WindowWidth, 0.4f * GameSettings.WindowHeight);

        private RectangleShape Shape { get; init; }

        private PlayersList _playersList;

        public BottomLeftPanel(IGameLobbyContent gameContent) : base(gameContent)
        {
            var rectangle = new FloatRect(Position, Size);
            this.View = new BottomLeftView(rectangle);

            this.Shape = new RectangleShape(Size);

            this.Shape.Position = Position;
            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 2.0f;

            this._playersList = new PlayersList(gameContent);
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);

            this._playersList.Draw(drawer);
        }

        public override void Handle(MouseEvent e) 
        {
            this._playersList.Handle(e);
        }

        public override void Handle(GameLobbyResultMapInfoChanged e) 
        {
            this._playersList.Handle(e);
        }

        public override void Handle(GameLobbyResultPlayersInfoChanged e) { }
    }
}
