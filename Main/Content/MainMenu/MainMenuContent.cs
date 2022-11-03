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
    public class MainMenuContent : IWindowContent
    {
        private readonly IGameState _gameState;

        private TexturedButton StartButton { get; init; }
        private TexturedButton ExitButton { get; init; }

        public MainMenuContent(IGameState gameState)
        {
            _gameState = gameState;
            _gameState.RestartView();

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

        public void Update() { }
    }
}
