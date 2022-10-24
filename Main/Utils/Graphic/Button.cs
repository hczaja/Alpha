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
    internal class Button :
        IDrawable,
        IEventHandler<MouseEvent>
    {
        public string Name { get; init; }
        public RectangleShape Rectangle { get; init; }

        public Button(string name, Vector2f size, Vector2f position, Texture texture)
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

        public void Handle(MouseEvent e)
        {
            this.Rectangle.OutlineThickness = 0.0f;

            if (e.Type == MouseEventType.Move
                && MouseEvent.IsMouseEventRaisedIn(this.Rectangle.GetGlobalBounds(), e))
            {
                this.Rectangle.OutlineThickness = 2.0f;
            }
        }
    }
}
