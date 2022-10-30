using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    internal abstract class GamePanel : 
        IDrawable, 
        IEventHandler<MouseEvent>,
        IEventHandler<KeyboardEvent>
    {
        public FloatRect Rectangle { get; init; }
        public GamePanelView View { get; init; }
        protected readonly IGameState _gameState;

        public GamePanel(IGameState gameState) => (_gameState) = (gameState);

        public abstract void Draw(RenderTarget drawer);

        public abstract void Handle(MouseEvent e);
        public abstract void Handle(KeyboardEvent e);
        public abstract void Update();
    }
}
