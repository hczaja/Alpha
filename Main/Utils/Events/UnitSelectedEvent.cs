using Main.Content.Game.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public class UnitSelectedEvent
    {
        public Unit Unit { get; init; }

        public UnitSelectedEvent(Unit unit) => (Unit) = (unit);
    }
}
