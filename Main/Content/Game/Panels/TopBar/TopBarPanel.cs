﻿using Main.Content.Game.Turns;
using Main.Utils;
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

namespace Main.Content.Game.Panels
{
    internal class TopBarPanel : GamePanel
    {
        public static readonly Vector2f Position = new Vector2f(0f, 0f);
        public static readonly Vector2f Size = new Vector2f(GameSettings.WindowWidth, 0.05f * GameSettings.WindowHeight);

        private RectangleShape Shape { get; init; }

        private TexturedButton NextTurnButton { get; init; }

        public TopBarPanel(IGameContent gameContent, ITurnManager turnManager) : base(gameContent, turnManager)
        {
            this.Rectangle = new FloatRect(Position, Size);
            this.View = new RightBarView(this.Rectangle);

            this.Shape = new RectangleShape(Size);
            this.Shape.Position = Position;
            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineColor = Color.Red;
            this.Shape.OutlineThickness = 1.0f;

            this.NextTurnButton = new NextTurnButton(this.Shape);
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this.Shape);

            this.NextTurnButton.Draw(drawer);
        }

        public override void Handle(MouseEvent e) 
        {
            if (e.Type == MouseEventType.ButtonPressed
                    && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this.NextTurnButton.Rectangle.GetGlobalBounds(), e)) 
                {
                    this._gameContent.ProcessNextTurn();
                }
            }
        }

        public override void Handle(KeyboardEvent e) { }

        public override void Handle(NewTurnEvent e) { }

        public override void Update() { }
    }
}