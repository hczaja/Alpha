using Main.Content.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Notifications
{
    public class GameLobbyNotificationService : INotificationService
    {
        private Queue<Notification?> _notifications;

        public GameLobbyNotificationService()
        {
            this._notifications = new Queue<Notification?>();
        }

        public void DequeueNotification(int playerId) => this._notifications.Dequeue();

        public void EnqueueNotification(int playerId, Notification notification) => this._notifications.Enqueue(notification);

        public bool HasNotificationsFor(int playerId) => this._notifications.Any();

        public bool TryGetNotification(int playerId, out Notification? result)
        {
            result = this._notifications.FirstOrDefault();
            return result is not null;
        }
    }
}
