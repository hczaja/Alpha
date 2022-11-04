using Main.Utils;
using Main.Utils.Graphic;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    internal class BackButton : TextButton
    {
        private static Vector2f _size = new Vector2f(48f, 24f);
        private static Vector2f _position = new Vector2f(0f, GameSettings.WindowHeight) + new Vector2f(_fontSpacing, - (24f + _fontSpacing));

        public BackButton()
            : base("Back!", _size, _position, "Back!")
        { }
    }
}
