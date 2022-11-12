using Main.Content.Game.GameObjects.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class ResourceSelectedEvent
    {
        public Resource Resource { get; init; }

        public ResourceSelectedEvent(Resource resource) => (Resource) = (resource);
    }
}
