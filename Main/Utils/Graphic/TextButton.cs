using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Graphic
{
    public class TextButton : IDrawable
    {
        public string Name { get; init; }
        public RectangleShape Rectangle { get; init; }
        public Text Text { get; init; }

        public static readonly uint _buttonFontSize = 16;
        public static readonly uint _fontSpacing = 8;

        public TextButton(string name, Vector2f size, Vector2f position, string text)
        {
            this.Name = name;
            this.Rectangle = new RectangleShape();

            this.Rectangle.Size = size;
            this.Rectangle.Position = position;
            this.Rectangle.FillColor = Color.Black;

            this.Rectangle.OutlineThickness = 1f;
            this.Rectangle.OutlineColor = Color.White;

            this.Text = new Text(text, GameSettings.Font, _buttonFontSize);
            this.Text.Position = this.Rectangle.Position + new Vector2f(_fontSpacing, _fontSpacing);
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this.Rectangle);
            drawer.Draw(this.Text);
        }
    }
}
