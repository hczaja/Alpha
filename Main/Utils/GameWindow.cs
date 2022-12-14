using Main.Utils.Events;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils
{
    internal class GameWindow
    {
        private readonly RenderWindow _window;
        private readonly IGameState _gameState;

        public GameWindow()
        {
            this._window = new RenderWindow(
                new VideoMode(
                    GameSettings.WindowWidth,
                    GameSettings.WindowHeight),
                GameSettings.GameTitle);

            this._window.SetKeyRepeatEnabled(enable: false);

            this._window.Closed += _window_Closed;
            this._window.KeyPressed += _window_KeyPressed;
            this._window.KeyReleased += _window_KeyReleased;
            this._window.MouseButtonPressed += _window_MouseButtonPressed;
            this._window.MouseButtonReleased += _window_MouseButtonReleased;
            this._window.MouseMoved += _window_MouseMoved;

            this._gameState = new GameState(this);
        }

        private void _window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            //Console.WriteLine($"{DateTime.Now} - {nameof(_window_MouseMoved)}");
            this._gameState.Handle(new MouseEvent(MouseEventType.MouseMoved, e.X, e.Y, Mouse.Button.Left));
        }

        private void _window_Closed(object? sender, EventArgs e)
        {
            this.TryClose();
        }

        private void _window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} - {nameof(_window_MouseButtonReleased)} - {e.Button}");
            this._gameState.Handle(new MouseEvent(MouseEventType.ButtonReleased, e.X, e.Y, e.Button));
        }

        private void _window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} - {nameof(_window_MouseButtonPressed)} - {e.Button}");
            this._gameState.Handle(new MouseEvent(MouseEventType.ButtonPressed, e.X, e.Y, e.Button));
        }

        private void _window_KeyReleased(object? sender, KeyEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} - {nameof(_window_KeyReleased)} - {e.Code}");
            this._gameState.Handle(new KeyboardEvent(KeyboardEventType.KeyReleased, e.Code));
        }

        private void _window_KeyPressed(object? sender, KeyEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} - {nameof(_window_KeyPressed)} - {e.Code}");
            this._gameState.Handle(new KeyboardEvent(KeyboardEventType.KeyPressed, e.Code));
        }

        public void Clear() => this._window.Clear(Color.Black);

        public void DispatchEvents() => this._window.DispatchEvents();

        public void Display() => this._window.Display();

        public void Draw() => this._gameState.Draw(this._window);

        public void Update() => this._gameState.Update();

        public bool IsOpen() => this._window.IsOpen;

        public void TryClose()
        {
            if (this._gameState.TrySave())
            {
                this._window.Close();
            }
        }

        public void RestartView()
        {
            this._window.SetView(this._window.DefaultView);
        }
    }
}
