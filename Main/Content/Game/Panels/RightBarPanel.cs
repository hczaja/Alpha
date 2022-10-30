using Main.Utils;
using Main.Utils.Events;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    internal class RightBarPanel : GamePanel
    {
        public static readonly Vector2f Position = new Vector2f(GameSettings.WindowWidth * 0.8f, 0f);
        public static readonly Vector2f Size = new Vector2f(0.2f * GameSettings.WindowWidth, 0.8f * GameSettings.WindowHeight);

        private RectangleShape Shape { get; init; }

        public RightBarPanel(IGameState gameState) : base(gameState)
        {
            this.Rectangle = new FloatRect(Position, Size);
            this.View = new RightBarView(this.Rectangle);

            this.Shape = new RectangleShape(Size);
            this.Shape.Position = Position;
            this.Shape.FillColor = Color.White;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 2.0f;
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);
        }

        public override void Handle(MouseEvent e) { }

        public override void Handle(KeyboardEvent e) { }

        public override void Update() { }
    }
}
