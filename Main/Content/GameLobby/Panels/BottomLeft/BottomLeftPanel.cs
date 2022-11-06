using Main.Content.Lobby;
using Main.Utils;
using Main.Utils.Events;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    internal class BottomLeftPanel : GameLobbyPanel
    {
        public static readonly Vector2f Position = new Vector2f(Gap, TopLeftPanel.Position.Y + TopLeftPanel.Size.Y + Gap);
        public static readonly Vector2f Size = new Vector2f(0.6f * GameSettings.WindowWidth, 0.4f * GameSettings.WindowHeight);

        private RectangleShape Shape { get; init; }

        public BottomLeftPanel(IGameLobbyContent gameContent) : base(gameContent)
        {
            var rectangle = new FloatRect(Position, Size);
            this.View = new BottomLeftView(rectangle);

            this.Shape = new RectangleShape(Size);

            this.Shape.Position = Position;
            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 2.0f;
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);
        }

        public override void Handle(MouseEvent e) { }

        public override void Handle(GameLobbyResultChanged e) { }
    }
}
