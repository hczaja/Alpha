using Main.Content.Game.Factions;
using Main.Content.Game.Resources;
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
        public Supplies Supplies { get; init; }

        public Player(FactionType factionType, Income startingIncome) => (ID, Faction, Supplies) = (++_id, new Faction(factionType), new Supplies(startingIncome));

        public Income CalculateIncome() => new Income() { Gold = 100 };

        public void Handle(NewTurnEvent e)
        {
            var income = this.CalculateIncome();
            this.Supplies.Update(income);

            // refresh actions for all units
        }
    }
}
