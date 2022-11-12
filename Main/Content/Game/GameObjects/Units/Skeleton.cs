using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.GameObjects.Units
{
    internal class Skeleton : Unit
    {
        private readonly Texture _texture = new Texture("Assets/Units/Skeleton.png");

        public Skeleton(Vector2f position)
        {
            var size = new Vector2f(Cell._CellSizeX, Cell._CellSizeY);

            this.DrawBox = new RectangleShape(size);
            this.DrawBox.Position = position;
            this.DrawBox.Texture = _texture;

            this.CollideBox = new RectangleShape(size);
            this.CollideBox.Position = position;
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.Draw(this.DrawBox);
        }

        public override Texture GetUnitTextureLayer() => _texture;
    }
}
