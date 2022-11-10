using Main.Content.Common;
using Main.Content.Game.Turns;
using Main.Content.Lobby;
using Main.Utils;
using Main.Utils.Events;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    internal class GameLobbyNotificationPanel : GameLobbyPanel
    {
        public static readonly Vector2f Position = new Vector2f(0f, 0f);
        public static readonly Vector2f Size = new Vector2f(GameSettings.WindowWidth, GameSettings.WindowHeight);

        private RectangleShape Background { get; init; }

        private readonly INotificationService _notificationService;
        private Notification? currentNotification;

        public GameLobbyNotificationPanel(IGameLobbyContent gameContent, INotificationService notificationService) : base(gameContent)
        {
            var rectangle = new FloatRect(Position, Size);
            this.View = new GameLobbyNotificationView(rectangle);

            this.Background = new RectangleShape(Size);
            this.Background.Position = Position;
            this.Background.FillColor = Color.Black;

            this._notificationService = notificationService;
            this.currentNotification = null;
        }

        public override void Draw(RenderTarget drawer)
        {
            if (this.currentNotification is not null)
            {
                drawer.SetView(this.View);

                if (this.currentNotification.DrawBackground) drawer.Draw(this.Background);
                this.currentNotification.Draw(drawer);
            }
        }

        public override void Handle(MouseEvent e) 
        {
            this.currentNotification?.Handle(e);
        }

        public void Update() 
        { 
            this._notificationService.TryGetNotification(-1, out var notification);
            
            if (this.currentNotification != notification)
            {
                this.currentNotification = notification;
            }
        }

        public override void Handle(GameLobbyResultMapInfoChanged e) { }
    }
}
