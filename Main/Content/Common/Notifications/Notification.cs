using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Common
{
    public abstract class Notification : IDrawable, IEventHandler<MouseEvent>
    {
        public bool DrawBackground { get; init; }

        protected readonly RectangleShape _background;
        protected readonly Text _title;

        public static readonly uint _titleFontSize = 32;
        public static readonly uint _contentFontSize = 24;
        public static readonly uint _fontSpacing = 8;

        public Notification(RectangleShape background, Text title, bool drawBackground)
            => (_background, _title, DrawBackground) = (background, title, drawBackground);

        public virtual void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);
            drawer.Draw(this._title);
        }

        public abstract void Handle(MouseEvent e);
    }
}
