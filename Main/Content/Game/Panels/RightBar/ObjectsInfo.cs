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
    public class ObjectsInfo : IDrawable, 
        IEventHandler<BuildingSelectedEvent>,
        IEventHandler<TerrainSelectedEvent>,
        IEventHandler<UnitSelectedEvent>,
        IEventHandler<ResourceSelectedEvent>,
        IEventHandler<MouseEvent>
    {
        private readonly IGameContent _gameContent;

        private readonly RectangleShape _background;

        public static readonly float Margin = RightBarPanel.Size.Y * 0.05f;
        public static readonly Vector2f TabSize = new Vector2f(RightBarPanel.Size.X / 4f, RightBarPanel.Size.X / 4f);

        private BuildingBlockInfoTab _buildingInfo;
        private UnitBlockInfoTab _unitInfo;
        private ResourceBlockInfoTab _resourceInfo;
        private TerrainBlockInfoTab _terrainInfo;

        public ObjectsInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(new Vector2f(RightBarPanel.Size.X, RightBarPanel.Size.Y * 0.65f));
            this._background.Position = RightBarPanel.Position + new Vector2f(0.0f, Options.BackgroundSize.Y + Minimap.BackgroundSize.Y);
            this._background.FillColor = Color.Black;
            this._background.OutlineThickness = 1f;
            this._background.OutlineColor = Color.Red;

            this._buildingInfo = new BuildingBlockInfoTab(gameContent);
            this._unitInfo = new UnitBlockInfoTab(gameContent);
            this._resourceInfo = new ResourceBlockInfoTab(gameContent);
            this._terrainInfo = new TerrainBlockInfoTab(gameContent);
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);

            this._buildingInfo.Draw(drawer);
            this._unitInfo.Draw(drawer);
            this._resourceInfo.Draw(drawer);
            this._terrainInfo.Draw(drawer);
        }

        public void Handle(BuildingSelectedEvent e)
        {
            this._buildingInfo.Handle(e);
        }

        public void Handle(TerrainSelectedEvent e)
        {
            this._terrainInfo.Handle(e);
        }

        public void Handle(UnitSelectedEvent e)
        {
            this._unitInfo.Handle(e);
        }

        public void Handle(ResourceSelectedEvent e)
        {
            this._resourceInfo.Handle(e);
        }

        public void Handle(MouseEvent e)
        {
            this._buildingInfo.Handle(e);
            this._unitInfo.Handle(e);
            this._terrainInfo.Handle(e);
            this._resourceInfo.Handle(e);
        }
    }
}
