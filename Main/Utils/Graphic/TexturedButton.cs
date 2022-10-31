using Main.Utils.Events;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Graphic
{
    public class TexturedButton : IDrawable
    {
        public string Name { get; init; }
        public RectangleShape Rectangle { get; init; }

        public TexturedButton(string name, Vector2f size, Vector2f position, Texture texture)
        {
            this.Name = name;
            this.Rectangle = new RectangleShape();

            this.Rectangle.Size = size;
            this.Rectangle.Position = position;
            this.Rectangle.Texture = texture;
            this.Rectangle.OutlineColor = SFML.Graphics.Color.White;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this.Rectangle);
        }
    }
}
