using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Resources
{
    public record struct Income
    {
        // Special
        public int Food { get; init; }
        public int Essence { get; init; }

        // Tactical
        public int Powder { get; init; }
        public int Steel { get; init; }
        
        // Valuable
        public int Gold { get; init; }
        public int Crystal { get; init; }
        
        // Construction
        public int Wood { get; init; }
        public int Stone { get; init; }
    }

    public class Supplies
    {
        public int Food { get; private set; }
        public int Essence { get; private set; }
        public int Powder { get; private set; }
        public int Steel { get; private set; }
        public int Gold { get; private set; }
        public int Crystal { get; private set; }
        public int Wood { get; private set; }
        public int Stone { get; private set; }

        public Supplies(Income startingIncome)
        {
            this.Update(startingIncome);
        }

        public void Update(Income income)
        {
            this.Food += income.Gold;
            this.Essence += income.Gold;
            this.Powder += income.Gold;
            this.Steel += income.Gold;
            this.Gold += income.Gold;
            this.Crystal += income.Gold;
            this.Wood += income.Gold;
            this.Stone += income.Gold;
        }
    }
}
