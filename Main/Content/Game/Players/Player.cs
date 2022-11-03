using Main.Content.Game.Factions;
using Main.Content.Game.Turns;
using Main.Utils.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    public class Player : IEventHandler<NewTurnEvent>
    {
        private static int _id;
        public int ID { get; init; }

        public Faction Faction { get; init; }

        public Player(FactionType factionType) => (ID, Faction) = (++_id, new Faction(factionType));

        public void Handle(NewTurnEvent e)
        { 
            // refresh actions for all units
        }
    }
}
