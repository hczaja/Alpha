using Main.Content.Game.Notifications;
using Main.Content.Game.Turns;
using Main.Utils;
using Main.Utils.Events;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    internal class NotificationPanel : GamePanel
    {
        public static readonly Vector2f Position = new Vector2f(0f, 0f);
        public static readonly Vector2f Size = new Vector2f(GameSettings.WindowWidth, GameSettings.WindowHeight);

        private RectangleShape Background { get; init; }

        private readonly INotificationService _notificationService;
        private Notification? currentNotification;

        public NotificationPanel(IGameState gameState, ITurnManager turnManager, INotificationService notificationService) : base(gameState, turnManager)
        {
            this.Rectangle = new FloatRect(Position, Size);
            this.View = new NotificationView(this.Rectangle);

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

        public override void Handle(KeyboardEvent e) { }

        public override void Handle(NewTurnEvent e) { }

        public override void Update() 
        { 
            int currentPlayerId = this._turnMangaer.GetCurrentPlayer().ID;
            this._notificationService.TryGetNotification(currentPlayerId, out var notification);
            
            if (this.currentNotification != notification)
            {
                this.currentNotification = notification;
            }
        }
    }
}
