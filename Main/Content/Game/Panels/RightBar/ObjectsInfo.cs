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

        public static readonly Vector2f InfoBlockSize = new Vector2f(RightBarPanel.Size.X, (RightBarPanel.Size.Y - RightBarPanel.Size.X) / 4f);

        private BuildingBlockInfo _buildingInfo;
        private UnitBlockInfo _unitInfo;
        private ResourceBlockInfo _resourceInfo;
        private TerrainBlockInfo _terrainInfo;

        public ObjectsInfo(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._buildingInfo = new BuildingBlockInfo(gameContent);
            this._unitInfo = new UnitBlockInfo(gameContent);
            this._resourceInfo = new ResourceBlockInfo(gameContent);
            this._terrainInfo = new TerrainBlockInfo(gameContent);
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
            this._terrainInfo.Handle(e);
            this._unitInfo.Handle(e);
            this._resourceInfo.Handle(e);
        }
    }
}
