using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.GameObjects.Buildings
{
    internal class Castle : Building
    {
        private readonly Texture _texture = new Texture("Assets/Buildings/Castle.png");

        public Castle(Vector2f position, Player owner) : base(position, owner)
        {
            this.DrawBox = new RectangleShape(
                new Vector2f(Cell._CellSizeX, Cell._CellSizeY));
            this.DrawBox.Position = position;
            this.DrawBox.Texture = _texture;
            this.DrawBox.FillColor = Color.Green;

            this.CollideBox = new RectangleShape(
                new Vector2f(Cell._CellSizeX, Cell._CellSizeY));
            this.CollideBox.Position = position;
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.Draw(this.DrawBox);
        }

        public override Texture GetBuildingTextureLayer() => _texture;

        public override string ToString() => nameof(Tower);

    }
}
