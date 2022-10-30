using Main.Content.Game.GameObjects.Buildings;
using Main.Content.Game.GameObjects.Resources;
using Main.Content.Game.GameObjects.Units;
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

        public Unit Unit { get; private set; }
        public Resource Resource { get; private set; }
        public Building Building { get; private set; }

        public Cell(int i, int j, Terrain terrain)
        {
            this.Rectangle = new RectangleShape();
            this.Terrain = terrain;

            this.Rectangle.Size = new Vector2f(_CellSizeX, _CellSizeY);
            this.Rectangle.Position = new Vector2f(i * _CellSizeX, j * _CellSizeY);
            this.Rectangle.FillColor = this.Terrain.GetColor();
            this.Rectangle.OutlineColor = Color.White;
            this.Rectangle.OutlineThickness = 2.0f;
        }

        public void AddUnit(Unit u) => this.Unit = u;
        public void RemoveUnit() => this.Unit = null;

        public void AddResource(Resource r) => this.Resource = r;
        public void RemoveResource() => this.Resource = null;

        public void AddBuilding(Building b) => this.Building = b;
        public void RemoveBuilding() => this.Building = null;

        public void Draw(RenderTarget drawer)
        {
            this.Rectangle.OutlineColor = Color.White;

            if (this.Selected)
            {
                this.Rectangle.OutlineColor = Color.Red;
            }
            
            drawer.Draw(this.Rectangle);

            this.Unit?.Draw(drawer);
            this.Resource?.Draw(drawer);
            this.Building?.Draw(drawer);
        }

        public void Select()
        {
            this.Selected = true;
            Console.WriteLine($"Selected Cell " +
                $"[{this.Rectangle.Position.X / _CellSizeX},{this.Rectangle.Position.Y / _CellSizeY}]" +
                $" - {this.Terrain.Name}" +
                $" - {this.Unit?.ToString()}" +
                $" - {this.Building?.ToString()}" +
                $" - {this.Resource?.ToString()}");
        }

        public void Unselect() => this.Selected = false;
    }
}
