using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Terrains
{
    internal sealed record Terrain
    {
        public string Name { get; }
        public TerrainType Type { get; }

        public Terrain(TerrainType type) => (Name, Type) = (type.ToString(), type);

        // replace colors with textures
        private static Dictionary<TerrainType, Color> _terrainToColor =
            new Dictionary<TerrainType, Color>()
            {
                { TerrainType.Unknown, Color.Black },
                { TerrainType.Water, new Color(75, 115, 156) }, // blue
                { TerrainType.Dirt, new Color(107, 69, 33) }, // brown
                { TerrainType.Grass, new Color(56, 102, 58) }, // green
            };

        public static TerrainType[] GetAllTerrainTypes() => new TerrainType[] { TerrainType.Unknown, TerrainType.Water, TerrainType.Dirt, TerrainType.Grass };

        public Color GetColor() => _terrainToColor[this.Type];
    }
}
