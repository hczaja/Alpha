using Main.Utils;
using Main.Utils.Graphic;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Lobby
{
    internal class StartButton : TextButton
    {
        private static Vector2f _size = new Vector2f(48f, 24f);
        private static Vector2f _position = new Vector2f(GameSettings.WindowWidth, GameSettings.WindowHeight) - new Vector2f(48f + _fontSpacing, 24f + _fontSpacing);

        public StartButton()
            : base("Start!", _size, _position, "Start!")
        { }
    }
}
