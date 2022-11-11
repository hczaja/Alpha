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
        public Player Owner { get; init; }

        public Building(Vector2f position, Player owner) => (Position, Owner) = (position, owner);

        protected RectangleShape? DrawBox { get; init; }
        protected RectangleShape? CollideBox { get; init; }

        public abstract void Draw(RenderTarget drawer);
        public virtual Color GetBuildingColorLayer() => this.Owner.Faction.GetFactionColor();
    }
}
