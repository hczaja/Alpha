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
        private readonly IGameState _gameState;
        
        private readonly GameCamera Camera;

        private readonly CentralPanel CentralPanel;
        private readonly RightBarPanel RightBarPanel;
        private readonly BottomBarPanel BottomBarPanel;

        public GameContent(IGameState gameState)
        {
            _gameState = gameState;

            this.Camera = new GameCamera(CentralPanel.Size.X, CentralPanel.Size.Y);

            this.CentralPanel = new CentralPanel(this.Camera, this._gameState);
            this.RightBarPanel = new RightBarPanel(this._gameState);
            this.BottomBarPanel = new BottomBarPanel(this._gameState);
        }

        public void Draw(RenderTarget drawer) 
        {
            this.CentralPanel.Draw(drawer);
            this.RightBarPanel.Draw(drawer);
            this.BottomBarPanel.Draw(drawer);
        }

        public void Handle(MouseEvent e)
        {
            this.Camera.Handle(e);
            this.CentralPanel.Handle(e);
            this.RightBarPanel.Handle(e);
            this.BottomBarPanel.Handle(e);
        }

        public void Handle(KeyboardEvent e) 
        { 
            if (e.Type == KeyboardEventType.KeyPressed && e.Key == Keyboard.Key.Escape)
            {
                this._gameState.Handle(new WindowContentChangedEvent(WindowContentEventType.MainMenu));
            }

            this.CentralPanel.Handle(e);
            this.RightBarPanel.Handle(e);
            this.BottomBarPanel.Handle(e);
        }

        public void Update() 
        {
            this.CentralPanel.Update();
            this.RightBarPanel.Update();
            this.BottomBarPanel.Update();
        }
    }
}
