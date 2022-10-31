using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Notifications
{
    public record Notification : IDrawable, IEventHandler<MouseEvent>
    {
        public bool DrawBackground { get; init; }

        public void Draw(RenderTarget drawer)
        {

        }

        public void Handle(MouseEvent e)
        {

        }
    }
}
