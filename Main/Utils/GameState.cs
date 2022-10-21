using Main.Utils.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils
{
    internal class GameState : 
        IEventHandler<MouseEvent>,
        IEventHandler<KeyboardEvent>
    {
        public void Draw() { }
        public void Update() { }

        public void Handle(MouseEvent e)
        {
            // event contains the current position of the mouse cursor relative to the window!

            throw new NotImplementedException();
        }

        public void Handle(KeyboardEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
