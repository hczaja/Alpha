using Main.Content.Game;
using Main.Content.Game.Terrains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Main.Content.Common.MapManager
{
    [XmlRoot]
    public class MapRegistry
    {
        [XmlElement("Map")]
        public List<Map>? Maps { get; set; }

        public MapRegistry()
        {
            this.Maps = new List<Map>();
        }
    }

    public class Map
    {
        [XmlAttribute]
        public string? Name { get; set; }

        [XmlAttribute]
        public GridSize GridSize { get; set; }

        [XmlAttribute]
        public int Players { get; set; }

        [XmlElement]
        public MapData? MapData { get; set; }

        public Map()
        {
            this.MapData = new MapData();
        }
        
        //[XmlAttribute]
        //public string WinCondition { get; set; }

        //[XmlAttribute]
        //public string Difficulty { get; set; }
    }

    public class MapData
    {
        [XmlElement("MapField")]
        public List<MapField>? Fields { get; set; }

        public MapData()
        {
            this.Fields = new List<MapField>();
        }
    }

    public class MapField
    {
        [XmlAttribute]
        public int Column { get; set; }

        [XmlAttribute]
        public int Row { get; set; }

        [XmlAttribute]
        public TerrainType TerrainType { get; set; }
    }
}
