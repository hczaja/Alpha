using Main.Content.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class UpdateMinimapEvent
    {
        public Grid Map { get; init; }

        public UpdateMinimapEvent(Grid map) => (Map) = (map);
    }
}
