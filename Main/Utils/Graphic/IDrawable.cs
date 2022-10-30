using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Graphic
{
    public interface IDrawable
    {
        public void Draw(RenderTarget drawer);
    }
}
