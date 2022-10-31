using Main.Utils.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Turns
{
    internal class TurnManager : ITurnManager
    {
        private Player[] PlayerOrder { get; init; }
        private Player currentPlayer;

        public TurnManager(Player[] players)
        {
            this.PlayerOrder = players;
            this.currentPlayer = this.PlayerOrder.First();
        }

        public Player GetCurrentPlayer() => this.currentPlayer;

        public Player GetNextPlayer()
        {
            int index = Array.IndexOf(this.PlayerOrder, this.currentPlayer);
            if (index != this.PlayerOrder.Length - 1)
            {
                this.currentPlayer = this.PlayerOrder[index + 1];
            }
            else
            {
                this.ProcessNewTurn();
            }

            return this.currentPlayer;
        }

        private void ProcessNewTurn()
        {
            ITurnManager.turnCounter++;

            foreach (var p in this.PlayerOrder)
            {
                p.Handle(new NewTurnEvent(ITurnManager.turnCounter, p));
            }

            this.currentPlayer = this.PlayerOrder.First();
        }
    }
}
