using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.MainMenu
{
    internal sealed class ExitButton : Button
    {
        private static Texture ExitButtonTexture = new Texture("Assets/Button_ExitGame.png");

        private static Vector2f ExitButtonSizeVector = new Vector2f(60.0f, 20.0f);
        private static Vector2f ExitButtonPositionVector = new Vector2f(20.0f, 50.0f);

        public ExitButton()
            : base(
                "Exit Game",
                ExitButtonSizeVector,
                ExitButtonPositionVector,
                ExitButtonTexture)
        { }
    }
}
