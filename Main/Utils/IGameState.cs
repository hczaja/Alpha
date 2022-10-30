using Main.Utils.Events;
using Main.Utils.Graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils
{
    public interface IGameState :
        IDrawable,
        IEventHandler<MouseEvent>,
        IEventHandler<KeyboardEvent>,
        IEventHandler<WindowFocusEvent>,
        IEventHandler<WindowContentChangedEvent>
    {
        void RestartView();
        void Update();
        bool TrySave();
    }
}
