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
        public static Dictionary<TerrainType, Color> TerrainToColor =
            new Dictionary<TerrainType, Color>()
            {
                { TerrainType.Unknown, Color.Black },
                { TerrainType.Water, new Color(75, 115, 156) },
                { TerrainType.Dirt, new Color(125, 104, 91) },
                { TerrainType.Grass, new Color(56, 102, 58) },
                { TerrainType.Sand, new Color(189, 194, 143) },
                { TerrainType.Mud, new Color(102, 117, 97) },
                { TerrainType.Rocks, new Color(105, 102, 100) },
            };

        public static TerrainType[] GetAllTerrainTypes() => 
            new TerrainType[] 
            { 
                TerrainType.Water, 
                TerrainType.Dirt, 
                TerrainType.Grass,
                TerrainType.Sand,
                TerrainType.Mud,
                TerrainType.Rocks
            };

        public Color GetColor() => TerrainToColor[this.Type];
    }
}
