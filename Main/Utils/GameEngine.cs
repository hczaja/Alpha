using Main.Utils.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils
{
    internal class GameEngine
    {
        private GameWindow _gameWindow;
        private GameClock _gameClock;

        public GameEngine()
        {
            this._gameWindow = new GameWindow();
            this._gameClock = new GameClock();
        }

        public void Run()
        {
            while (this._gameWindow.IsOpen())
            {
                this._gameWindow.DispatchEvents();

                this._gameClock.Update();
                if (this._gameClock.IsUpdated())
                {
                    this._gameWindow.Update();
                    
                    this._gameWindow.Clear();
                    this._gameWindow.Draw();
                    this._gameWindow.Display();

                    this._gameClock.Restart();
                }
            }
        }
    }
}
