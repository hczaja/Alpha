﻿using Main.Content.Game.Notifications;
using Main.Content.Game.Panels;
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
    internal class GameContent : IWindowContent
    {
        private readonly IGameState _gameState;
        
        private readonly GameCamera _camera;

        private readonly CentralPanel _centralPanel;
        private readonly RightBarPanel _rightBarPanel;
        private readonly BottomBarPanel _bottomBarPanel;
        private readonly NotificationPanel _notificationPanel;

        private readonly ITurnManager _turnManager;
        private readonly INotificationService _notificationService;

        public GameContent(IGameState gameState)
        {
            _gameState = gameState;

            var players = new Player[2];
            players[0] = new Player();
            players[1] = new Player();

            this._camera = new GameCamera(CentralPanel.Size.X, CentralPanel.Size.Y);
            this._turnManager = new TurnManager(players);

            this._centralPanel = new CentralPanel(this._camera, this._gameState, this._turnManager);
            this._rightBarPanel = new RightBarPanel(this._gameState, this._turnManager);
            this._bottomBarPanel = new BottomBarPanel(this._gameState, this._turnManager);

            this._notificationService = new NotificationService(players);
            this._notificationPanel = new NotificationPanel(this._gameState, this._turnManager, this._notificationService);
        }

        public void Draw(RenderTarget drawer) 
        {
            this._centralPanel.Draw(drawer);
            this._rightBarPanel.Draw(drawer);
            this._bottomBarPanel.Draw(drawer);
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

            this._camera.Handle(e);
            this._centralPanel.Handle(e);
            this._rightBarPanel.Handle(e);
            this._bottomBarPanel.Handle(e);
        }

        public void Handle(KeyboardEvent e) 
        { 
            if (e.Type == KeyboardEventType.KeyPressed && e.Key == Keyboard.Key.Escape)
            {
                this._gameState.Handle(new WindowContentChangedEvent(WindowContentEventType.MainMenu));
            }
            
            if (e.Type == KeyboardEventType.KeyPressed && e.Key == Keyboard.Key.Enter)
            {
                var currentPlayer = this._turnManager.GetCurrentPlayer();
                this._notificationService.EnqueueNotification(0, new NewTurnNotification(this._notificationService, currentPlayer));
            }

            this._centralPanel.Handle(e);
            this._rightBarPanel.Handle(e);
            this._bottomBarPanel.Handle(e);
        }

        public void Update() 
        {
            this._centralPanel.Update();
            this._rightBarPanel.Update();
            this._bottomBarPanel.Update();
            this._notificationPanel.Update();
        }
    }
}
