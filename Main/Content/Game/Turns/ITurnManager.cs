using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Turns
{
    public interface ITurnManager
    {
        static int turnCounter = 1;

        Player GetNextPlayer();
        Player GetCurrentPlayer();
    }
}
