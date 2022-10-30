using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.GameObjects.Resources
{
    internal class Tree : Resource
    {
        private readonly Texture _texture = new Texture("Assets/Resources/Tree.png");

        public Tree(Vector2f position)
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

        public override string ToString() => nameof(Tree);
    }
}
