using Main.Utils;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    internal abstract class GamePanelView : View
    {
        protected readonly FloatRect _viewRectangle;

        public abstract void Update(int playerIndex);

        public GamePanelView(FloatRect viewRect) 
            : base(new FloatRect(
                new Vector2f(0.0f, 0.0f), 
                new Vector2f(GameSettings.WindowWidth, GameSettings.WindowHeight)))
            => (_viewRectangle) = (viewRect);
    }
}
