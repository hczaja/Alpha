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

        public Player() => (ID) = (++_id);

        public void Handle(NewTurnEvent e)
        {
            
        }
    }
}
