using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Turns
{
    internal class TurnManager
    {
        public static int turnCounter = 0;

        private Player[] PlayerOrder { get; init; }
        public Player CurrentPlayer { get; private set; }

        public TurnManager(Player[] players)
        {
            this.PlayerOrder = players;
            this.CurrentPlayer = this.PlayerOrder.First();
        }

        public void ProcessNextPlayer()
        {
            int index = Array.IndexOf(this.PlayerOrder, this.CurrentPlayer);
            if (index != this.PlayerOrder.Length - 1)
            {
                this.CurrentPlayer = this.PlayerOrder[index + 1];
            }
            else
            {
                this.ProcessNewTurn();
            }
        }

        private void ProcessNewTurn()
        {
            turnCounter++;

            foreach (var p in this.PlayerOrder)
            {
                p.Handle(new NewTurn(turnCounter));
            }

            this.CurrentPlayer = this.PlayerOrder.First();
        }
    }
}
