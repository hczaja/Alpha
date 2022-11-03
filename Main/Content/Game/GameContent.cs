using Main.Content.Game.Factions;
using Main.Content.Game.Notifications;
using Main.Content.Game.Panels;
using Main.Content.Game.Resources;
using Main.Content.Game.Turns;
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

        public GameContent(IGameState gameState)
        {
            _gameState = gameState;

            var players = new Player[2];
            var startingIncome = new Income() { Gold = 500 };
            players[0] = new Player(FactionType.Undeads, startingIncome);
            players[1] = new Player(FactionType.Dwarves, startingIncome);

            this._turnManager = new TurnManager(players);

            this._centralPanel = new CentralPanel(this, this._turnManager);
            this._rightBarPanel = new RightBarPanel(this, this._turnManager);
            this._bottomBarPanel = new BottomBarPanel(this, this._turnManager);
            this._topBarPanel = new TopBarPanel(this, this._turnManager);

            this._notificationService = new NotificationService(players);
            this._notificationPanel = new NotificationPanel(this, this._turnManager, this._notificationService);
        }

        public void ProcessNextTurn()
        {                        
            var nextPlayer = this._turnManager.GetNextPlayer();

            this._notificationService.EnqueueNotification(nextPlayer.ID, new NewTurnNotification(this._notificationService, nextPlayer));
            this._topBarPanel.Handle(new NewTurnEvent(ITurnManager.turnCounter, nextPlayer));
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
    }
}
