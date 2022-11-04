using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Resources
{
    public record struct Income
    {
        public int Gold { get; init; }
    }

    public class Supplies
    {
        public int Gold { get; private set; }

        public Supplies(Income startingIncome)
        {
            this.Gold = startingIncome.Gold;
        }

        public void Update(Income income)
        {
            this.Gold += income.Gold;
        }
    }
}
