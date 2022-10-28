using Main.Content.Game.Terrains;
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
        public Terrain Terrain { get; init; }

        public bool Selected { get; private set; }

        public Cell(int i, int j)
        {
            this.Rectangle = new RectangleShape();

            // temporary solution
            var randomType = Terrain.GetAllTerrainTypes()[Random.Shared.Next(0, 4)];
            this.Terrain = new Terrain(randomType);

            this.Rectangle.Size = new Vector2f(_CellSizeX, _CellSizeY);
            this.Rectangle.Position = new Vector2f(i * _CellSizeX, j * _CellSizeY);
            this.Rectangle.FillColor = this.Terrain.GetColor();
            this.Rectangle.OutlineColor = Color.White;
            this.Rectangle.OutlineThickness = 2.0f;
        }

        public void Draw(RenderTarget drawer)
        {
            this.Rectangle.OutlineColor = Color.White;

            if (this.Selected)
            {
                this.Rectangle.OutlineColor = Color.Red;
            }
            
            drawer.Draw(this.Rectangle);
        }

        public void Select()
        {
            this.Selected = true;
            Console.WriteLine($"Selected Cell [{this.Rectangle.Position.X / _CellSizeX},{this.Rectangle.Position.Y / _CellSizeY}] - {this.Terrain.Name}");
        }

        public void Unselect() => this.Selected = false;
    }
}
