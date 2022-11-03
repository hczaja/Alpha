using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    public class NextTurnButton : TexturedButton
    {
        private static Texture NextTurnButtonTexture = new Texture("Assets/Utils/Button_NextTurn.png");

        private static Vector2f NextTurnButtonSizeVector = new Vector2f(24.0f, 24.0f);
        private static Vector2f NextTurnButtonPositionShiftVector = new Vector2f(5.0f, 5.0f);

        public NextTurnButton(RectangleShape topBarShape)
            : base(
                  "Next Turn",
                  NextTurnButtonSizeVector,
                  topBarShape.Size - NextTurnButtonSizeVector - NextTurnButtonPositionShiftVector,
                  NextTurnButtonTexture)
        {
            this.Rectangle.OutlineColor = Color.White;
            this.Rectangle.OutlineThickness = 1f;
        }
    }
}
