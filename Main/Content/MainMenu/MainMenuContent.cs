using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.MainMenu
{
    internal class MainMenuContent : IWindowContent
    {
        private readonly GameState _gameState;

        private Button StartButton { get; init; }
        private Button ExitButton { get; init; }

        public MainMenuContent(GameState gameState)
        {
            _gameState = gameState;

            this.StartButton = new StartButton();
            this.ExitButton = new ExitButton();
        }

        public void Draw(RenderTarget drawer)
        {
            this.StartButton.Draw(drawer);
            this.ExitButton.Draw(drawer);
        }

        public void Handle(MouseEvent e)
        {
            this.StartButton.Handle(e);
            this.ExitButton.Handle(e);

            if (e.Type == MouseEventType.ButtonPressed
                && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this.StartButton.Rectangle.GetGlobalBounds(), e))
                {
                    this._gameState.Handle(
                        new WindowContentChangedEvent(WindowContentEventType.Game));
                }
                else if (MouseEvent.IsMouseEventRaisedIn(this.ExitButton.Rectangle.GetGlobalBounds(), e))
                {
                    this._gameState.Handle(
                        new WindowContentChangedEvent(WindowContentEventType.Exit));
                }
            }
        }

        public void Handle(KeyboardEvent e) { }

        public void Update(RenderTarget window) { }
    }
}
