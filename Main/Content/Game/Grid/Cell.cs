using Main.Content.Game.GameObjects.Buildings;
using Main.Content.Game.GameObjects.Resources;
using Main.Content.Game.GameObjects.Units;
using Main.Content.Game.Terrains;
using Main.Utils.Camera;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    internal class Cell : IDrawable, IEventHandler<NewTurnEvent>
    {
        public static readonly float _CellSizeX = 32;
        public static readonly float _CellSizeY = 32;

        public RectangleShape Rectangle { get; init; }
        public Terrain Terrain { get; init; }

        private bool _selected;

        public Unit? Unit { get; private set; }
        public Resource? Resource { get; private set; }
        public Building? Building { get; private set; }

        public Dictionary<Direction, Cell?> Surrounding { get; init; }

        private Player _currentPlayer;
        private FogOfWar _fogOfWar;

        private readonly IGameContent _gameContent;

        public Cell(int i, int j, Player startingPlayer, Terrain terrain, IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this.Rectangle = new RectangleShape();
            this.Terrain = terrain;

            this._currentPlayer = startingPlayer;
            this._fogOfWar = new FogOfWar();

            this.Rectangle.Size = new Vector2f(_CellSizeX, _CellSizeY);
            this.Rectangle.Position = new Vector2f(i * _CellSizeX, j * _CellSizeY);
            this.Rectangle.OutlineColor = Color.Transparent;
            this.Rectangle.OutlineThickness = 1.0f;

            this.Surrounding = new Dictionary<Direction, Cell?>();
        }

        public void AddUnit(Unit u) => this.Unit = u;
        public void RemoveUnit() => this.Unit = null;

        public void AddResource(Resource r) => this.Resource = r;
        public void RemoveResource() => this.Resource = null;

        public void AddBuilding(Building b) => this.Building = b;
        public void RemoveBuilding() => this.Building = null;

        public void DiscoverFor(int playerId) => this._fogOfWar.DiscoverFor(playerId);
        public void HideFor(int playerId) => this._fogOfWar.HideFor(playerId);

        public void Handle(NewTurnEvent e)
        {
            this._currentPlayer = e.PlayerInfo;
        }

        public void Draw(RenderTarget drawer)
        {
            this.Rectangle.FillColor = this.Terrain.GetColor();

            if (!this._fogOfWar.IsVisibleFor(this._currentPlayer.ID))
            {
                this.Rectangle.FillColor = FogOfWar.GetFogColor();
                drawer.Draw(this.Rectangle);
                return;
            }

            this.Rectangle.OutlineColor = Color.Transparent;

            if (this._selected)
            {
                this.Rectangle.OutlineColor = Color.Red;
            }
            
            drawer.Draw(this.Rectangle);

            this.Unit?.Draw(drawer);
            this.Resource?.Draw(drawer);
            this.Building?.Draw(drawer);
        }

        public Color GetCellColorLayer()
        {
            var result = FogOfWar.GetFogColor();

            if (!this._fogOfWar.IsVisibleFor(this._currentPlayer.ID))
                return result;

            result = this.Terrain.GetColor();

            if (this.Building is not null) result = this.Building.GetBuildingColorLayer();

            return result;
        }

        public void Select()
        {
            this._selected = true;

            bool isVisible = this._fogOfWar.IsVisibleFor(this._currentPlayer.ID);

            this._gameContent.Handle(new TerrainSelectedEvent(isVisible ? this.Terrain : null));
            this._gameContent.Handle(new UnitSelectedEvent(isVisible ? this.Unit : null));
            this._gameContent.Handle(new BuildingSelectedEvent(isVisible ? this.Building : null));
            this._gameContent.Handle(new ResourceSelectedEvent(isVisible ? this.Resource : null));
        }

        public void Unselect()
        {
            this.Rectangle.OutlineColor = Color.Transparent;

            this._selected = false;

            this._gameContent.Handle(new TerrainSelectedEvent(null));
            this._gameContent.Handle(new UnitSelectedEvent(null));
            this._gameContent.Handle(new BuildingSelectedEvent(null));
            this._gameContent.Handle(new ResourceSelectedEvent(null));
        }

        public bool IsOccupied()
        {
            if (this.Unit is not null) return true;
            if (this.Building is not null) return true;
            if (this.Resource is not null) return true;

            return false;
        }
    }
}
