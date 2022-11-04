using Main.Utils;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels.TopBar.Resources
{
    public abstract class ResourceInfo : IDrawable
    {
        private readonly RectangleShape _rectangle;
        private readonly Text _text;

        public ResourceInfo(int resourceAmount, int resourceIncomeAmount, Texture resourceTexture, Vector2f resourceInfoPosition)
        {
            this._rectangle = new RectangleShape(new Vector2f(24f, 24f));
            this._rectangle.Texture = resourceTexture;
            this._rectangle.Position = resourceInfoPosition;

            this._text = new Text($":{resourceAmount.ToString("0000")}(+{resourceIncomeAmount})", GameSettings.Font, 24);
            this._text.Position = this._rectangle.Position + this._rectangle.Size + new Vector2f(0f, -24.0f);
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._rectangle);
            drawer.Draw(this._text);
        }
    }
}
