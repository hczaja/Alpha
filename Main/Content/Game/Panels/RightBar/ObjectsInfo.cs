using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels.RightBar
{
    public class BuildingBlockInfo : IDrawable, IEventHandler<BuildingSelectedEvent>
    {
        private readonly IGameContent _gameContent;
        public static readonly Vector2f BuildingBlockPosition = new Vector2f(RightBarPanel.Position.X, RightBarPanel.Position.Y + RightBarPanel.Size.X);
        private static Texture BuildingBlockTexture = new Texture("Assets/Utils/BuildingTemplate.png");

        private RectangleShape _background;
        private RectangleShape _image;

        public BuildingBlockInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(ObjectsInfo.InfoBlockSize);
            this._background.Position = BuildingBlockPosition;
            this._background.FillColor = Color.Black;
            this._background.OutlineColor = Color.Red;
            this._background.OutlineThickness = 2f;

            this._image = new RectangleShape(new Vector2f(ObjectsInfo.InfoBlockSize.Y, ObjectsInfo.InfoBlockSize.Y));
            this._image.Position = this._background.Position;
            this._image.FillColor = Color.White;
            this._image.Texture = BuildingBlockTexture;
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
    }

    public class UnitInfo : IDrawable
    {
        private readonly IGameContent _gameContent;
        public static readonly Vector2f UnitBlockPosition = BuildingBlockInfo.BuildingBlockPosition + new Vector2f(0f, ObjectsInfo.InfoBlockSize.Y);
        private static Texture UnitBlockTexture = new Texture("Assets/Utils/UnitTemplate.png");

        private RectangleShape _background;
        private RectangleShape _image;

        public UnitInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(ObjectsInfo.InfoBlockSize);
            this._background.Position = UnitBlockPosition;
            this._background.FillColor = Color.Black;
            this._background.OutlineColor = Color.Red;
            this._background.OutlineThickness = 2f;

            this._image = new RectangleShape(new Vector2f(ObjectsInfo.InfoBlockSize.Y, ObjectsInfo.InfoBlockSize.Y));
            this._image.Position = this._background.Position;
            this._image.Texture = UnitBlockTexture;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
            drawer.Draw(this._image);
        }
    }

    public class ResourceInfo : IDrawable
    {
        private readonly IGameContent _gameContent;
        public static readonly Vector2f ResourceBlockInfo = UnitInfo.UnitBlockPosition + new Vector2f(0f, ObjectsInfo.InfoBlockSize.Y);
        private static Texture ResourceBlockTexture = new Texture("Assets/Utils/ResourceTemplate.png");

        private RectangleShape _background;
        private RectangleShape _image;

        public ResourceInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(ObjectsInfo.InfoBlockSize);
            this._background.Position = ResourceBlockInfo;
            this._background.FillColor = Color.Black;
            this._background.OutlineColor = Color.Red;
            this._background.OutlineThickness = 2f;

            this._image = new RectangleShape(new Vector2f(ObjectsInfo.InfoBlockSize.Y, ObjectsInfo.InfoBlockSize.Y));
            this._image.Position = this._background.Position;
            this._image.Texture = ResourceBlockTexture;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
            drawer.Draw(this._image);
        }
    }

    public class TerrainInfo : IDrawable
    {
        private readonly IGameContent _gameContent;
        public static readonly Vector2f TerrainBlockInfo = ResourceInfo.ResourceBlockInfo + new Vector2f(0f, ObjectsInfo.InfoBlockSize.Y);
        private static Texture TerrainBlockTexture = new Texture("Assets/Utils/TerrainTemplate.png");

        private RectangleShape _background;
        private RectangleShape _image;

        public TerrainInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(ObjectsInfo.InfoBlockSize);
            this._background.Position = TerrainBlockInfo;
            this._background.FillColor = Color.Black;
            this._background.OutlineColor = Color.Red;
            this._background.OutlineThickness = 2f;

            this._image = new RectangleShape(new Vector2f(ObjectsInfo.InfoBlockSize.Y, ObjectsInfo.InfoBlockSize.Y));
            this._image.Position = this._background.Position;
            this._image.Texture = TerrainBlockTexture;
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
            drawer.Draw(this._image);
        }
    }

    public class ObjectsInfo : IDrawable, 
        IEventHandler<BuildingSelectedEvent>
    {
        private readonly IGameContent _gameContent;

        public static readonly Vector2f InfoBlockSize = new Vector2f(RightBarPanel.Size.X, (RightBarPanel.Size.Y - RightBarPanel.Size.X) / 4f);

        private BuildingBlockInfo _buildingInfo;
        private UnitInfo _unitInfo;
        private ResourceInfo _resourceInfo;
        private TerrainInfo _terrainInfo;

        public ObjectsInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._buildingInfo = new BuildingBlockInfo(gameContent);
            this._unitInfo = new UnitInfo(gameContent);
            this._resourceInfo = new ResourceInfo(gameContent);
            this._terrainInfo = new TerrainInfo(gameContent);
        }

        public void Draw(RenderTarget drawer)
        {
            this._buildingInfo.Draw(drawer);
            this._unitInfo.Draw(drawer);
            this._resourceInfo.Draw(drawer);
            this._terrainInfo.Draw(drawer);
        }

        public void Handle(BuildingSelectedEvent e)
        {
            this._buildingInfo.Handle(e);
        }
    }
}
