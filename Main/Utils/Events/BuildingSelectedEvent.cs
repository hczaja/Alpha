using Main.Content.Game.GameObjects.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class BuildingSelectedEvent
    {
        public Building Building { get; init; }

        public BuildingSelectedEvent(Building building) => (Building) = (building);
    }
}
