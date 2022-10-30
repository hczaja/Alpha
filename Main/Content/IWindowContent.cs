using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content
{
    internal interface IWindowContent :
        IDrawable,
        IEventHandler<MouseEvent>,
        IEventHandler<KeyboardEvent>
    {
        public void Update();
    }
}
