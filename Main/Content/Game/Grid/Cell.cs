using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    internal class Cell : IDrawable
    {
        public static readonly float _CellSizeX = 32;
        public static readonly float _CellSizeY = 32;

        public RectangleShape Rectangle { get; init; }

        public Cell(int i, int j)
        {
            this.Rectangle = new RectangleShape();

            this.Rectangle.Size = new Vector2f(_CellSizeX, _CellSizeY);
            this.Rectangle.Position = new Vector2f(i * _CellSizeX, j * _CellSizeY);
            this.Rectangle.FillColor = SFML.Graphics.Color.Transparent;
            this.Rectangle.OutlineColor = SFML.Graphics.Color.White;
            this.Rectangle.OutlineThickness = 2.0f;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this.Rectangle);
        }
    }
}
