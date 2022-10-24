using Main.Utils;
using Main.Utils.Events;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    internal class GameContent : IWindowContent
    {
        private readonly GameState _gameState;

        private Grid Grid { get; init; }

        public GameContent(GameState gameState)
        {
            _gameState = gameState;

            this.Grid = new Grid(GridSize.Small);
        }

        public void Draw(RenderTarget drawer) 
        {
            this.Grid.Draw(drawer);
        }

        public void Handle(MouseEvent e) { }

        public void Handle(KeyboardEvent e) { }

        public void Update() { }
    }
}
