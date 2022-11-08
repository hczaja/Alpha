using Main.Content.Common;
using Main.Content.Common.MapManager;
using Main.Content.GameLobby.Notifications;
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
        IEventHandler<GameLobbyResultMapInfoChanged>
    {
        public GameLobbyData GetGameLobbyData();
        public Map GetMapInfo();
    }

    public class GameLobbyContent : IGameLobbyContent
    {
        private readonly IGameState _gameState;

        private readonly TopLeftPanel _topLeftPanel;
        private readonly TopRightPanel _topRightPanel;
        private readonly BottomLeftPanel _bottomLeftPanel;
        private readonly GameLobbyNotificationPanel _notificationPanel;

        private readonly TextButton _startButton;
        private readonly TextButton _backButton;

        private GameLobbyData _gameLobbyData;
        private readonly IPlayerManager _playerManager;

        private readonly INotificationService _notificationService;

        public GameLobbyContent(IGameState gameState)
        {
            this._gameState = gameState;
            this._gameState.RestartView();

            this._playerManager = new PlayerManager(0);
            this._gameLobbyData = new GameLobbyData(this._playerManager);

            this._topLeftPanel = new TopLeftPanel(this);
            this._bottomLeftPanel = new BottomLeftPanel(this, this._playerManager);
            this._topRightPanel = new TopRightPanel(this);

            this._notificationService = new GameLobbyNotificationService();
            this._notificationPanel = new GameLobbyNotificationPanel(this, this._notificationService);

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

            this._notificationPanel.Draw(drawer);
        }

        public void Handle(MouseEvent e) 
        {
            if (this._notificationService.HasNotificationsFor(-1))
            {
                this._notificationPanel.Handle(e);
                return;
            }

            if (e.Type == MouseEventType.ButtonPressed 
                && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this._startButton.Rectangle.GetGlobalBounds(), e))
                {
                    if (!this._playerManager.IsAtLeastOneHumanPlayer())
                    {
                        this._notificationService.EnqueueNotification(-1, 
                            new ProblemWithStartGameNotification(this._notificationService, "At least one player must be human!"));
                        return;
                    }

                    if (!this._playerManager.AreAtLeastTwoDifferentTeams())
                    {
                        this._notificationService.EnqueueNotification(-1,
                            new ProblemWithStartGameNotification(this._notificationService, "There must be at least two different teams!"));
                        return;
                    }

                    if (!this._playerManager.AreUniqueFactions())
                    {
                        this._notificationService.EnqueueNotification(-1,
                            new ProblemWithStartGameNotification(this._notificationService, "There must be every faction unique!"));
                        return;
                    }

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
            this._gameLobbyData.MapInfo = e.MapInfo;
            this._topLeftPanel.Handle(e);
            this._bottomLeftPanel.Handle(e);
        }

        public void Update() 
        {
            this._notificationPanel.Update();
        }

        public Map GetMapInfo() => this._gameLobbyData.MapInfo;
        public GameLobbyData GetGameLobbyData() => this._gameLobbyData;
    }
}
