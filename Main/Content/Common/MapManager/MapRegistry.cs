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
        [XmlElement("MapInfo")]
        public List<MapInfo>? Maps { get; set; }
    }

    public class MapInfo
    {
        [XmlAttribute]
        public string? Name { get; set; }

        [XmlAttribute]
        public string? Size { get; set; }

        [XmlAttribute]
        public int Players { get; set; }

        //[XmlAttribute]
        //public string WinCondition { get; set; }

        //[XmlAttribute]
        //public string Difficulty { get; set; }
    }
}
