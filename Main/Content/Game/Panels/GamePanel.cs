using Main.Content.Game.Turns;
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
        IEventHandler<KeyboardEvent>,
        IEventHandler<NewTurnEvent>
    {
        public FloatRect Rectangle { get; init; }
        public GamePanelView View { get; init; }

        protected readonly IGameState _gameState;
        protected readonly ITurnManager _turnMangaer;

        public GamePanel(IGameState gameState, ITurnManager turnManager) => (_gameState, _turnMangaer) = (gameState, turnManager);

        public abstract void Draw(RenderTarget drawer);

        public abstract void Handle(MouseEvent e);
        public abstract void Handle(KeyboardEvent e);
        public abstract void Handle(NewTurnEvent e);
        public abstract void Update();
    }
}
