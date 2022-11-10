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
        private static int _maxId;
        public int ID { get; init; }

        public static int GetMaxID() => _maxId;

        public Faction Faction { get; init; }
        public Supplies Supplies { get; init; }

        public Player(int id, FactionType factionType, Income startingIncome)
        {
            this.ID = id;
            _maxId = Math.Max(_maxId, this.ID);

            this.Faction = new Faction(factionType);
            this.Supplies = new Supplies(startingIncome);
        }

        public Income CalculateIncome() => new Income() { Gold = 100 };

        public void Handle(NewTurnEvent e)
        {
            var income = this.CalculateIncome();
            this.Supplies.Update(income);

            // refresh actions for all units
        }
    }
}
