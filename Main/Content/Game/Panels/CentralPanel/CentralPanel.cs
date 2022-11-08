﻿using Main.Content.Game.Turns;
using Main.Utils;
using Main.Utils.Camera;
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
    internal class CentralPanel : GamePanel
    {
        public static readonly Vector2f Position = new Vector2f(0f, 0.05f * GameSettings.WindowHeight);
        public static readonly Vector2f Size = new Vector2f(0.8f * GameSettings.WindowWidth, 0.75f * GameSettings.WindowHeight);

        private Grid Grid { get; init; }
        private readonly GameCamera _camera;

        public CentralPanel(IGameContent gameContent, ITurnManager turnManager) : base(gameContent, turnManager)
        {
            var map = gameContent.GetMapInfo();

            var gridSize = map.GridSize;
            this._camera = new GameCamera(Position, Size);

            var rectangle = new FloatRect(Position, Size);
            this.View = new CentralView(this._camera, rectangle, gridSize);
            this.Grid = new Grid(map, this._camera, this._turnManager);
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            this.Grid.Draw(drawer);
        }

        public override void Handle(MouseEvent e)
        {
            var rectangle = new FloatRect(Position, Size);
            if (MouseEvent.IsMouseEventRaisedIn(rectangle, e))
            {
                this._camera.Handle(e);
                this.Grid.Handle(e);
            }
        }

        public override void Handle(KeyboardEvent e) { }

        public override void Handle(NewTurnEvent e) 
        {
            (this.View as CentralView)!.ResetView();
            this.Grid.Handle(e);
        }

        public override void Update()
        {
            this.View.Update();
        }
    }
}
