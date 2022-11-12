using Main.Content.Game.Terrains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class TerrainSelectedEvent
    {
        public Terrain Terrain { get; init; }

        public TerrainSelectedEvent(Terrain terrain) => (Terrain) = (terrain);
    }
}
