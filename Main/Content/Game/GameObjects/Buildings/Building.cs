using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.GameObjects.Buildings
{
    internal abstract class Building : IDrawable
    {
        public Vector2f Position { get; protected set; }

        protected RectangleShape? DrawBox { get; init; }
        protected RectangleShape? CollideBox { get; init; }

        public abstract void Draw(RenderTarget drawer);
    }
}
