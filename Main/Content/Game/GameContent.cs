using Main.Content.Common;
using Main.Content.Common.MapManager;
using Main.Content.Game.Factions;
using Main.Content.Game.Notifications;
using Main.Content.Game.Panels;
using Main.Content.Game.Resources;
using Main.Content.Game.Turns;
using Main.Content.GameLobby.Panels;
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
    public interface IGameContent : IWindowContent
    {
        void ProcessNextTurn();
        Map GetMapInfo();
    }

    internal class GameContent : IGameContent
    {
        private readonly IGameState _gameState;
        
        private readonly CentralPanel _centralPanel;
        private readonly RightBarPanel _rightBarPanel;
        private readonly BottomBarPanel _bottomBarPanel;
        private readonly TopBarPanel _topBarPanel;
        private readonly NotificationPanel _notificationPanel;

        private readonly ITurnManager _turnManager;
        private readonly INotificationService _notificationService;

        private readonly GameLobbyData _gameLobbyData;

        public GameContent(IGameState gameState, GameLobbyData gameLobbyData)
        {
            this._gameState = gameState;
            this._gameState.RestartView();

            this._gameLobbyData = gameLobbyData;

            var playerManager = gameLobbyData.PlayerManager;
            var nonEmptyPlayers = playerManager.Players.Where(p => p.Type != PlayerType.Empty.ToString()).ToList();

            int playersAmont = nonEmptyPlayers.Count;

            var players = new Player[playersAmont];
            var startingIncome = new Income() { Gold = 500 };

            for (int index = 0; index < playersAmont; index++)
            {
                var faction = Enum.Parse<FactionType>(nonEmptyPlayers[index].Faction);
                players[index] = new Player(int.Parse(nonEmptyPlayers[index].Index), faction, startingIncome);
            }

            this._turnManager = new TurnManager(players);

            this._centralPanel = new CentralPanel(this, this._turnManager);
            this._rightBarPanel = new RightBarPanel(this, this._turnManager);
            this._bottomBarPanel = new BottomBarPanel(this, this._turnManager);
            this._topBarPanel = new TopBarPanel(this, this._turnManager);

            this._notificationService = new NotificationService(players);
            this._notificationPanel = new NotificationPanel(this, this._turnManager, this._notificationService);

            this._notificationService.EnqueueNotification(players[0].ID, new NewTurnNotification(this._notificationService, players[0]));
        }

        public void ProcessNextTurn()
        {                        
            var nextPlayer = this._turnManager.GetNextPlayer();

            this._notificationService.EnqueueNotification(nextPlayer.ID, new NewTurnNotification(this._notificationService, nextPlayer));

            var newTurnEvent = new NewTurnEvent(ITurnManager.turnCounter, nextPlayer);

            this._centralPanel.Handle(newTurnEvent);
            this._rightBarPanel.Handle(newTurnEvent);
            this._bottomBarPanel.Handle(newTurnEvent);
            this._topBarPanel.Handle(newTurnEvent);
        }

        public void Draw(RenderTarget drawer) 
        {
            this._centralPanel.Draw(drawer);
            this._rightBarPanel.Draw(drawer);
            this._bottomBarPanel.Draw(drawer);
            this._topBarPanel.Draw(drawer);
            this._notificationPanel.Draw(drawer);
        }

        public void Handle(MouseEvent e)
        {
            int currentPlayerId = this._turnManager.GetCurrentPlayer().ID;
            if (this._notificationService.HasNotificationsFor(currentPlayerId))
            {
                this._notificationPanel.Handle(e);
                return;
            }

            this._centralPanel.Handle(e);
            this._rightBarPanel.Handle(e);
            this._bottomBarPanel.Handle(e);
            this._topBarPanel.Handle(e);
        }

        public void Handle(KeyboardEvent e) 
        { 
            if (e.Type == KeyboardEventType.KeyPressed && e.Key == Keyboard.Key.Escape)
            {
                this._gameState.Handle(new WindowContentChangedEvent(WindowContentEventType.MainMenu));
            }

            this._centralPanel.Handle(e);
            this._rightBarPanel.Handle(e);
            this._bottomBarPanel.Handle(e);
            this._topBarPanel.Handle(e);
        }

        public void Update() 
        {
            this._centralPanel.Update();
            this._rightBarPanel.Update();
            this._bottomBarPanel.Update();
            this._notificationPanel.Update();
            this._topBarPanel.Update();
        }

        public Map GetMapInfo() => this._gameLobbyData.MapInfo;
    }
}
