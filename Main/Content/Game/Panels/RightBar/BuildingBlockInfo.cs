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
    public class BuildingBlockInfo : IDrawable,
        IEventHandler<BuildingSelectedEvent>,
        IEventHandler<MouseEvent>
    {
        private readonly IGameContent _gameContent;
        public static readonly Vector2f BlockInfoPosition = new Vector2f(RightBarPanel.Position.X, RightBarPanel.Position.Y + RightBarPanel.Size.X);
        private static Texture BuildingBlockTexture = new Texture("Assets/Utils/BuildingTemplate.png");

        private RectangleShape _background;
        private RectangleShape _image;

        public BuildingBlockInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(ObjectsInfo.InfoBlockSize);
            this._background.Position = BlockInfoPosition;
            this._background.FillColor = Color.Black;
            this._background.OutlineColor = Color.Red;
            this._background.OutlineThickness = 2f;

            this._image = new RectangleShape(new Vector2f(ObjectsInfo.InfoBlockSize.Y, ObjectsInfo.InfoBlockSize.Y));
            this._image.Position = this._background.Position;
            this._image.Texture = BuildingBlockTexture;
            this._image.OutlineColor = Color.Red;
            this._image.OutlineThickness = 2f;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
            drawer.Draw(this._image);
        }

        public void Handle(BuildingSelectedEvent e)
        {
            var texture = e.Building?.GetBuildingTextureLayer() ?? BuildingBlockTexture;
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
