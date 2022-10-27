using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Time
{
    internal class GameClock : Clock
    {
        private float _totalTimeBeforeUpdate;
        private float _totalTimeElapsed;
        private float _previousTotalTimeElapsed;

        private static readonly float TimeBeforeUpdate = 1f / GameSettings.FPS;

        public void Update()
        {
            this._totalTimeElapsed = this.ElapsedTime.AsSeconds();

            float deltaTime = this._totalTimeElapsed - this._previousTotalTimeElapsed;
            this._previousTotalTimeElapsed = this._totalTimeElapsed;

            this._totalTimeBeforeUpdate += deltaTime;
        }

        public bool IsUpdated() => this._totalTimeBeforeUpdate >= TimeBeforeUpdate;

        public new void Restart()
        {
            this._totalTimeBeforeUpdate = 0.0f;
        }
    }
}
