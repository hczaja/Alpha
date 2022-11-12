using Main.Content.Game.Panels.RightBar;
using Main.Content.Game.Turns;
using Main.Utils;
using Main.Utils.Events;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    internal class RightBarPanel : GamePanel, 
        IEventHandler<UpdateMinimapEvent>,
        IEventHandler<BuildingSelectedEvent>,
        IEventHandler<TerrainSelectedEvent>,
        IEventHandler<UnitSelectedEvent>,
        IEventHandler<ResourceSelectedEvent>
    {
        public static readonly Vector2f Position = new Vector2f(0.8f * GameSettings.WindowWidth, 0.05f * GameSettings.WindowHeight);
        public static readonly Vector2f Size = new Vector2f(0.2f * GameSettings.WindowWidth, 0.75f * GameSettings.WindowHeight);

        private RectangleShape Shape { get; init; }

        private readonly Minimap _minimap;
        private readonly ObjectsInfo _objectInfo;

        public RightBarPanel(IGameContent gameContent, ITurnManager turnManager) : base(gameContent, turnManager)
        {
            var rectangle = new FloatRect(Position, Size);
            this.View = new RightBarView(rectangle);

            this.Shape = new RectangleShape(Size);
            this.Shape.Position = Position;
            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 2.0f;

            this._minimap = new Minimap(this._gameContent);
            this._objectInfo = new ObjectsInfo(this._gameContent);
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);

            this._minimap.Draw(drawer);
            this._objectInfo.Draw(drawer);
        }

        public void Handle(UpdateMinimapEvent e) 
        {
            this._minimap.Handle(e);
        }
        
        public void Handle(BuildingSelectedEvent e) 
        {
            this._objectInfo.Handle(e);
        }
        
        public void Handle(TerrainSelectedEvent e) 
        {
            this._objectInfo.Handle(e);
        }
        
        public void Handle(UnitSelectedEvent e) 
        {
            this._objectInfo.Handle(e);
        }
        
        public void Handle(ResourceSelectedEvent e) 
        {
            this._objectInfo.Handle(e);
        }

        public override void Handle(MouseEvent e) { }

        public override void Handle(KeyboardEvent e) { }

        public override void Handle(NewTurnEvent e) { }

        public override void Update() { }
    }
}
