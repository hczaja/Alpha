using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils
{
    internal class GameSettings
    {
        public static readonly uint WindowWidth = 1024;
        public static readonly uint WindowHeight = 768;

        public static readonly string GameTitle = "Alpha";
        public static readonly Font Font = new Font("Assets/Fonts/rainyhearts.ttf");

        public static readonly int FPS = 30;
    }
}
