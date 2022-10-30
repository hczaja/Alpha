using Main.Content.Game.Turns;
using Main.Utils;
using Main.Utils.Camera;
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
    internal class CentralPanel : GamePanel
    {
        public static readonly Vector2f Position = new Vector2f(0f, 0f);
        public static readonly Vector2f Size = new Vector2f(0.8f * GameSettings.WindowWidth, 0.8f * GameSettings.WindowHeight);

        private Grid Grid { get; init; }

        public CentralPanel(GameCamera camera, IGameState gameState) : base(gameState)
        {
            this.Rectangle = new FloatRect(Position, Size);
            this.View = new CentralView(camera, this.Rectangle);

            this.Grid = new Grid(GridSize.Medium, camera);
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            this.Grid.Draw(drawer);
        }

        public override void Handle(MouseEvent e)
        {
            this.Grid.Handle(e);
        }

        public override void Handle(KeyboardEvent e) { }

        public override void Handle(NewTurnEvent e) { }

        public override void Update()
        {
            this.View.Update();
        }
    }
}
