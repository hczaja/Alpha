using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Common.MapManager
{
    public class MapInfo
    {
        public string Index { get; private init; }
        public string Name { get; private init; }
        public string Size { get; private init; }
        public string Players { get; private init; }

        public MapInfo(string index, string name, string size, string players)
            => (Index, Name, Size, Players) = (index, name, size, players);
    }
}
