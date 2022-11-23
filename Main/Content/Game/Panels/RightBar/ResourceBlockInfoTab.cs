using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels.RightBar
{
    public class ResourceBlockInfoTab : IDrawable,
        IEventHandler<ResourceSelectedEvent>,
        IEventHandler<MouseEvent>
    {
        private readonly IGameContent _gameContent;
        public static readonly Vector2f BlockInfoPosition = UnitBlockInfoTab.BlockInfoPosition + new Vector2f(ObjectsInfo.TabSize.X, 0f);
        private static Texture ResourceBlockTexture = new Texture("Assets/Utils/ResourceTemplate.png");

        private RectangleShape _background;
        private RectangleShape _image;

        public ResourceBlockInfoTab(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(ObjectsInfo.TabSize);
            this._background.Position = BlockInfoPosition;
            this._background.FillColor = Color.Black;
            this._background.OutlineThickness = 1f;
            this._background.OutlineColor = Color.Red;

            this._image = new RectangleShape(ObjectsInfo.TabSize);
            this._image.Position = this._background.Position;
            this._image.Texture = ResourceBlockTexture;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
            drawer.Draw(this._image);
        }

        public void Handle(ResourceSelectedEvent e)
        {
            var texture = e.Resource?.GetResourceTextureLayer() ?? ResourceBlockTexture;
            this._image.Texture = texture;
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed
                && e.Button == Mouse.Button.Left
                && MouseEvent.IsMouseEventRaisedIn(this._background.GetGlobalBounds(), e))
            {

            }
        }
    }
}
