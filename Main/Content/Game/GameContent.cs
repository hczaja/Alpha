using Main.Utils;
using Main.Utils.Camera;
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
        
        private readonly GameCamera Camera;
        private readonly GameView View;

        private Grid Grid { get; init; }

        public GameContent(GameState gameState)
        {
            _gameState = gameState;

            this.Camera = new GameCamera(GameSettings.WindowWidth, GameSettings.WindowHeight);
            this.View = new GameView(this.Camera);

            this.Grid = new Grid(GridSize.Small, this.Camera);
        }

        public void Draw(RenderTarget drawer) 
        {
            this.Grid.Draw(drawer);
        }
        public void Handle(MouseEvent e)
        {
            this.Camera.Handle(e);
            this.Grid.Handle(e);
        }

        public void Handle(KeyboardEvent e) 
        { 
        
        }

        public void Update(RenderTarget window) 
        {
            this.View.Update();
            window.SetView(this.View);
        }
    }
}
