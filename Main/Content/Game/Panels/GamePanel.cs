using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    internal abstract class GamePanel : IDrawable, IEventHandler<MouseEvent>
    {
        public FloatRect Rectangle { get; init; }
        public GamePanelView View { get; init; }

        public abstract void Draw(RenderTarget drawer);

        public abstract void Handle(MouseEvent e);
        public abstract void Update();
    }
}
