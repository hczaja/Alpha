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

        public GameWindow()
        {
            this._window = new RenderWindow(
                new VideoMode(
                    GameSettings.WindowWidth,
                    GameSettings.WindowHeight),
                GameSettings.GameTitle);

            this._window.Closed += (object? sender, EventArgs e) => this._window.Close();
            this._window.SetKeyRepeatEnabled(enable: false);
        }

        public void Clear() => this._window.Clear(Color.Black);
        public void DispatchEvents() => this._window.DispatchEvents();
        public void Display() => this._window.Display();
        public void Draw() { }
        public void Update() { }
        public bool IsOpen() => this._window.IsOpen;
    }
}
