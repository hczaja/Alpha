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
    public class TerrainBlockInfo : IDrawable,
        IEventHandler<TerrainSelectedEvent>,
        IEventHandler<MouseEvent>
    {
        private readonly IGameContent _gameContent;
        public static readonly Vector2f BlockInfoPosition = ResourceBlockInfo.BlockInfoPosition + new Vector2f(0f, ObjectsInfo.InfoBlockSize.Y);
        private static Texture TerrainBlockTexture = new Texture("Assets/Utils/TerrainTemplate.png");

        private RectangleShape _background;
        private RectangleShape _image;

        public TerrainBlockInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(ObjectsInfo.InfoBlockSize);
            this._background.Position = BlockInfoPosition;
            this._background.FillColor = Color.Black;
            this._background.OutlineColor = Color.Red;
            this._background.OutlineThickness = 2f;

            this._image = new RectangleShape(new Vector2f(ObjectsInfo.InfoBlockSize.Y, ObjectsInfo.InfoBlockSize.Y));
            this._image.Position = this._background.Position;
            this._image.Texture = TerrainBlockTexture;
            this._image.OutlineColor = Color.Red;
            this._image.OutlineThickness = 2f;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
            drawer.Draw(this._image);
        }

        public void Handle(TerrainSelectedEvent e)
        {
            if (e.Terrain is not null)
            {
                this._image.Texture = null;
                this._image.FillColor = e.Terrain.GetColor();
            }
            else
            {
                this._image.Texture = TerrainBlockTexture;
                this._image.FillColor = Color.White;
            }
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
