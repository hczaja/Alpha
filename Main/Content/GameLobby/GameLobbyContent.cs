using Main.Content.Common.MapManager;
using Main.Content.GameLobby.Panels;
using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Lobby
{
    public interface IGameLobbyContent : IWindowContent, 
        IEventHandler<GameLobbyResultMapInfoChanged>,
        IEventHandler<GameLobbyResultPlayersInfoChanged>
    {
        public Map GetMapInfo();
    }

    public class GameLobbyContent : IGameLobbyContent
    {
        private readonly IGameState _gameState;

        private readonly TopLeftPanel _topLeftPanel;
        private readonly TopRightPanel _topRightPanel;
        private readonly BottomLeftPanel _bottomLeftPanel;

        private TextButton _startButton { get; init; }
        private TextButton _backButton { get; init; }

        private GameLobbyResult gameLobbyResult;

        public GameLobbyContent(IGameState gameState)
        {
            _gameState = gameState;
            _gameState.RestartView();

            this.gameLobbyResult = new GameLobbyResult();

            this._topLeftPanel = new TopLeftPanel(this);
            this._bottomLeftPanel = new BottomLeftPanel(this);
            this._topRightPanel = new TopRightPanel(this);

            this._backButton = new BackButton();
            this._startButton = new StartButton();
        }

        public void Draw(RenderTarget drawer)
        {
            this._topLeftPanel.Draw(drawer);
            this._topRightPanel.Draw(drawer);
            this._bottomLeftPanel.Draw(drawer);

            this._backButton.Draw(drawer);
            this._startButton.Draw(drawer);
        }

        public void Handle(MouseEvent e) 
        {
            if (e.Type == MouseEventType.ButtonPressed 
                && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this._startButton.Rectangle.GetGlobalBounds(), e))
                {
                    this._gameState.Handle(
                        new WindowContentChangedEvent(WindowContentEventType.Game));
                }
                else if (MouseEvent.IsMouseEventRaisedIn(this._backButton.Rectangle.GetGlobalBounds(), e))
                {
                    this._gameState.Handle(
                        new WindowContentChangedEvent(WindowContentEventType.MainMenu));
                }
            }

            this._topLeftPanel.Handle(e);
            this._topRightPanel.Handle(e);
            this._bottomLeftPanel.Handle(e);
        }

        public void Handle(KeyboardEvent e) 
        {
            if (e.Type == KeyboardEventType.KeyPressed
                && e.Key == Keyboard.Key.Escape)
            {
                this._gameState.Handle(
                    new WindowContentChangedEvent(WindowContentEventType.MainMenu));
            }
        }

        public void Handle(GameLobbyResultMapInfoChanged e) 
        {
            this.gameLobbyResult.MapInfo = e.MapInfo;
            this._topLeftPanel.Handle(e);
            this._bottomLeftPanel.Handle(e);
        }
        
        public void Handle(GameLobbyResultPlayersInfoChanged e) 
        {

        }

        public void Update() { }

        public Map GetMapInfo() => this.gameLobbyResult.MapInfo;
    }
}
