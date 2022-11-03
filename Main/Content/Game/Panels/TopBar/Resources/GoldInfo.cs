using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels.TopBar.Resources
{
    public class GoldInfo : ResourceInfo
    {
        private static readonly Texture _texture = new Texture("Assets/Resources/Icon_Gold.png");

        public GoldInfo(int goldAmount, int goldIncome, Vector2f position) 
            : base(goldAmount, goldIncome, _texture, position)
        { }
    }
}
