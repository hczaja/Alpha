using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.GameObjects.Buildings
{
    internal class Tower : Building
    {
        private readonly Texture _texture = new Texture("Assets/Buildings/Tower.png");

        public Tower(Vector2f position)
        {
            this.DrawBox = new RectangleShape(
                new Vector2f(Cell._CellSizeX, 2 * Cell._CellSizeY));
            this.DrawBox.Position = position - new Vector2f(0.0f, Cell._CellSizeY);
            this.DrawBox.Texture = _texture;

            this.CollideBox = new RectangleShape(
                new Vector2f(Cell._CellSizeX, Cell._CellSizeY));
            this.CollideBox.Position = position;
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.Draw(this.DrawBox);
        }

        public override string ToString() => nameof(Tower);
    }
}
