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

        private readonly CentralPanel MapPanel;
        private readonly RightBarPanel RightBarPanel;
        private readonly BottomBarPanel BottomBarPanel;

        public GameContent(GameState gameState)
        {
            _gameState = gameState;

            this.Camera = new GameCamera(CentralPanel.Size.X, CentralPanel.Size.Y);

            this.MapPanel = new CentralPanel(this.Camera);
            this.RightBarPanel = new RightBarPanel();
            this.BottomBarPanel = new BottomBarPanel();
        }

        public void Draw(RenderTarget drawer) 
        {
            this.MapPanel.Draw(drawer);
            this.RightBarPanel.Draw(drawer);
            this.BottomBarPanel.Draw(drawer);
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
        }
    }
}
