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

        private readonly TexturedButton _startButton;
        private readonly TexturedButton _exitButton;

        public MainMenuContent(IGameState gameState)
        {
            _gameState = gameState;
            _gameState.RestartView();

            this._startButton = new StartButton();
            this._exitButton = new ExitButton();
        }

        public void Draw(RenderTarget drawer)
        {
            this._startButton.Draw(drawer);
            this._exitButton.Draw(drawer);
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed
                && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this._startButton.Rectangle.GetGlobalBounds(), e))
                {
                    this._gameState.Handle(
                        new WindowContentChangedEvent(WindowContentEventType.GameLobby));
                }
                else if (MouseEvent.IsMouseEventRaisedIn(this._exitButton.Rectangle.GetGlobalBounds(), e))
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
