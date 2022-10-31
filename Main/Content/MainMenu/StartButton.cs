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
    internal sealed class StartButton : TexturedButton
    {
        private static Texture StartButtonTexture = new Texture("Assets/Utils/Button_StartGame.png");

        private static Vector2f StartButtonSizeVector = new Vector2f(60.0f, 20.0f);
        private static Vector2f StartButtonPositionVector = new Vector2f(20.0f, 20.0f);

        public StartButton()
            : base(
                "Start Game",
                StartButtonSizeVector,
                StartButtonPositionVector,
                StartButtonTexture)
        { }
    }
}
