using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels.RightBar
{
    public class Options : IDrawable, IEventHandler<MouseEvent>
    {
        private readonly RectangleShape _background;
        private readonly IGameContent _gameContent;

        public static readonly Vector2f BackgroundSize = new Vector2f(RightBarPanel.Size.X, RightBarPanel.Size.Y * 0.05f);

        public Options(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(BackgroundSize);
            this._background.Position = RightBarPanel.Position;
            this._background.FillColor = Color.Black;
            this._background.OutlineThickness = 1f;
            this._background.OutlineColor = Color.Red;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
        }

        public void Handle(MouseEvent e)
        {
            
        }
    }
}
