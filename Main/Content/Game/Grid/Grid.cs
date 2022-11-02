using Main.Content.Game.Terrains;
using Main.Utils.Camera;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.Window;
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

    internal class Grid : IDrawable, IEventHandler<MouseEvent>
    {
        public readonly int _width;
        public readonly int _height;
        private readonly GameCamera _gameCamera;

        private Cell[,] Cells { get; init; }
        private Cell CurrentCell = null;

        public static (int, int) GetGridDimensions(GridSize size) => size switch
            {
                GridSize.Small => (16, 16),
                GridSize.Medium => (24, 24),
                GridSize.Large => (32, 32)
            };

        public Grid(GridSize size, GameCamera camera)
        {
            this._gameCamera = camera;

            (this._width, this._height) = GetGridDimensions(size);

            this.Cells = new Cell[_width, _height];
            this.InitializeCells();

            //this.Cells[5, 5].AddBuilding(new Tower(this.Cells[5, 5].Rectangle.Position));
        }

        public void Draw(RenderTarget drawer)
        {
            foreach (var cell in this.Cells)
            {
                if (cell == this.CurrentCell)
                {
                    continue;
                }

                cell.Draw(drawer);
            }

            if (this.CurrentCell is not null)
            {
                this.CurrentCell.Draw(drawer);
            }
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed && e.Button == Mouse.Button.Left)
            {
                float x = e.X + this._gameCamera.MoveX;
                float y = e.Y + this._gameCamera.MoveY;

                int i = (int)(x / Cell._CellSizeX);
                int j = (int)(y / Cell._CellSizeY);

                try
                {
                    var cell = this.Cells[i, j];

                    this.CurrentCell?.Unselect();

                    this.CurrentCell = cell;
                    this.CurrentCell.Select();
                }
                catch (IndexOutOfRangeException)
                {
                    this.CurrentCell?.Unselect();
                    this.CurrentCell = null;
                }
            }
        }

        private void InitializeCells()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    // temporary solution
                    var randomType = Terrain.GetAllTerrainTypes()[Random.Shared.Next(0, 3)];
                    this.Cells[i, j] = new Cell(i, j, new Terrain(randomType));
                }
            }

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    (int _i, int _j) bottomRight = (i + 1, j + 1);
                    if (bottomRight._i < _width && bottomRight._j < _height) this.Cells[i, j].Surrounding.Add(Direction.BottomRight, this.Cells[bottomRight._i, bottomRight._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.BottomRight, null);

                    (int _i, int _j) bottom = (i, j + 1);
                    if (bottom._j < _height) this.Cells[i, j].Surrounding.Add(Direction.Bottom, this.Cells[bottom._i, bottom._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.Bottom, null);

                    (int _i, int _j) bottomLeft = (i - 1, j + 1);
                    if (bottomLeft._i >= 0 && bottomLeft._j < _height) this.Cells[i, j].Surrounding.Add(Direction.BottomLeft, this.Cells[bottomLeft._i, bottomLeft._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.BottomLeft, null);

                    (int _i, int _j) left = (i - 1, j);
                    if (left._i >= 0) this.Cells[i, j].Surrounding.Add(Direction.Left, this.Cells[left._i, left._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.Left, null);

                    (int _i, int _j) topleft = (i - 1, j - 1);
                    if (topleft._i >= 0 && topleft._j >= 0) this.Cells[i, j].Surrounding.Add(Direction.TopLeft, this.Cells[topleft._i, topleft._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.TopLeft, null);

                    (int _i, int _j) top = (i, j - 1);
                    if (top._j >= 0) this.Cells[i, j].Surrounding.Add(Direction.Top, this.Cells[top._i, top._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.Top, null);

                    (int _i, int _j) topRight = (i + 1, j - 1);
                    if (topRight._i < _width && topRight._j >= 0) this.Cells[i, j].Surrounding.Add(Direction.TopRight, this.Cells[topRight._i, topRight._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.TopRight, null);

                    (int _i, int _j) right = (i + 1, j);
                    if (right._i < _width) this.Cells[i, j].Surrounding.Add(Direction.Right, this.Cells[right._i, right._j]);
                    else this.Cells[i, j].Surrounding.Add(Direction.Right, null);
                }
            }
        }
    }
}
