using Main.Utils.Graphic;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    internal enum GridSize
    {
        Small, Medium, Large
    }

    internal class Grid : IDrawable
    {
        public readonly int _width;
        public readonly int _height;

        private Cell[,] Cells { get; init; }

        public Grid(GridSize size)
        {
            (this._width, this._height) = size switch
            {
                GridSize.Small => (16, 16),
                GridSize.Medium => (24, 24),
                GridSize.Large => (32, 32)
            };

            this.Cells = new Cell[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    this.Cells[i, j] = new Cell(i, j);
                }
            }
        }

        public void Draw(RenderTarget drawer)
        {
            foreach (var cell in this.Cells)
            {
                cell.Draw(drawer);
            }
        }
    }
}
