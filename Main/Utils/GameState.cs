using Main.Content;
using Main.Content.Game;
using Main.Content.MainMenu;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils
{
    internal class GameState :
        IDrawable,
        IEventHandler<MouseEvent>,
        IEventHandler<KeyboardEvent>,
        IEventHandler<WindowFocusEvent>,
        IEventHandler<WindowContentChangedEvent>
    {
        private readonly GameWindow _windowHandler;
        private IWindowContent ActualContent { get; set; }

        public GameState(GameWindow windowHandler)
        {
            this._windowHandler = windowHandler;
            this.ActualContent = new MainMenuContent(this);
        }

        public void Draw(RenderTarget drawer) 
        { 
            this.ActualContent.Draw(drawer); 
        }

        public void Update() 
        { 
            this.ActualContent.Update(); 
        }

        public bool TrySave()
        {
            return true;
        }

        public void Handle(MouseEvent e)
        {
            // event contains the current position of the mouse cursor relative to the window!
            this.ActualContent.Handle(e);
        }

        public void Handle(KeyboardEvent e)
        {
            this.ActualContent.Handle(e);
        }

        public void Handle(WindowFocusEvent e) { }

        public void Handle(WindowContentChangedEvent e)
        {
            switch (e.Type)
            {
                case WindowContentEventType.MainMenu:
                    this.ActualContent = new MainMenuContent(this);
                    break;
                case WindowContentEventType.Game:
                    this.ActualContent = new GameContent(this);
                    break;
                case WindowContentEventType.Exit:
                    this._windowHandler.TryClose();
                    break;
                case WindowContentEventType.Unknown:
                default:
                    break;
            }
        }
    }
}
