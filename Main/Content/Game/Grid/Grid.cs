using Main.Content.Game.GameObjects.Buildings;
using Main.Content.Game.GameObjects.Resources;
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

        public Grid(GridSize size, GameCamera camera)
        {
            this._gameCamera = camera;

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
                    // temporary solution
                    var randomType = Terrain.GetAllTerrainTypes()[Random.Shared.Next(0, 3)];
                    this.Cells[i, j] = new Cell(i, j, new Terrain(randomType));
                }
            }

            this.Cells[5, 5].AddBuilding(new Tower(this.Cells[5, 5].Rectangle.Position));
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
    }
}
