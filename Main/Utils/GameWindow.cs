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
        private readonly GameState _gameState;

        public GameWindow()
        {
            this._window = new RenderWindow(
                new VideoMode(
                    GameSettings.WindowWidth,
                    GameSettings.WindowHeight),
                GameSettings.GameTitle);

            this._window.SetKeyRepeatEnabled(enable: false);

            this._window.Closed += _window_Closed;
            this._window.GainedFocus += _window_GainedFocus;
            this._window.LostFocus += _window_LostFocus;
            this._window.KeyPressed += _window_KeyPressed;
            this._window.KeyReleased += _window_KeyReleased;
            this._window.MouseButtonPressed += _window_MouseButtonPressed;
            this._window.MouseButtonReleased += _window_MouseButtonReleased;
            this._window.MouseMoved += _window_MouseMoved;

            this._gameState = new GameState();
        }

        private void _window_Closed(object? sender, EventArgs e)
        {
            // Ask user for save the current game state
            this._window.Close();
        }

        private void _window_LostFocus(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _window_GainedFocus(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            this._gameState.Handle(new MouseEvent(MouseEventType.Move, e.X, e.Y, default));
        }

        private void _window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            this._gameState.Handle(new MouseEvent(MouseEventType.ButtonReleased, e.X, e.Y, e.Button));
        }

        private void _window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            this._gameState.Handle(new MouseEvent(MouseEventType.ButtonPressed, e.X, e.Y, e.Button));
        }

        private void _window_KeyReleased(object? sender, KeyEventArgs e)
        {
            this._gameState.Handle(new KeyboardEvent(KeyboardEventType.KeyReleased, e.Code));
        }

        private void _window_KeyPressed(object? sender, KeyEventArgs e)
        {
            this._gameState.Handle(new KeyboardEvent(KeyboardEventType.KeyPressed, e.Code));
        }

        public void Clear() => this._window.Clear(Color.Black);
        public void DispatchEvents() => this._window.DispatchEvents();
        public void Display() => this._window.Display();
        public void Draw() => this._gameState.Draw();
        public void Update() => this._gameState.Update();
        public bool IsOpen() => this._window.IsOpen;
    }
}
