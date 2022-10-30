using Main.Content.Game.Panels;
using Main.Utils;
using Main.Utils.Camera;
using Main.Utils.Events;
using SFML.Graphics;
using SFML.Window;
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

        private readonly MapPanel MapPanel;
        private readonly RightBarPanel RightBarPanel;

        public GameContent(GameState gameState)
        {
            _gameState = gameState;

            this.Camera = new GameCamera(MapPanel.Size.X, MapPanel.Size.Y);

            this.MapPanel = new MapPanel(this.Camera);
            this.RightBarPanel = new RightBarPanel();
        }

        public void Draw(RenderTarget drawer) 
        {
            this.MapPanel.Draw(drawer);
            this.RightBarPanel.Draw(drawer);
        }

        public void Handle(MouseEvent e)
        {
            this.Camera.Handle(e);
            this.MapPanel.Handle(e);
        }

        public void Handle(KeyboardEvent e) 
        { 
            if (e.Type == KeyboardEventType.KeyPressed && e.Key == Keyboard.Key.Escape)
            {
                this._gameState.Handle(new WindowContentChangedEvent(WindowContentEventType.MainMenu));
            }
        }

        public void Update() 
        {
            this.MapPanel.Update();
            this.RightBarPanel.Update();
        }
    }
}
