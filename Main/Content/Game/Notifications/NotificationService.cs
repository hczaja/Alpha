using Main.Content.Common;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Notifications
{
    public class NotificationService : INotificationService
    {
        private Dictionary<int, Queue<Notification?>> _notifications;

        public NotificationService(Player[] players)
        {
            this._notifications = new Dictionary<int, Queue<Notification?>>();
            
            foreach (var player in players)
            {
                this._notifications.Add(player.ID, new Queue<Notification?>());
            }
        }

        public bool HasNotificationsFor(int playerId) => this._notifications[playerId].Any();
        
        public void DequeueNotification(int playerId) => this._notifications[playerId].Dequeue();

        public void EnqueueNotification(int playerId, Notification notification)
        {
            if (playerId == -1)
            {
                foreach (var notifications in _notifications)
                {
                    notifications.Value.Enqueue(notification);
                }
            }
            else
            {
                this._notifications[playerId].Enqueue(notification);
            }
        }

        public bool TryGetNotification(int playerId, out Notification? result)
        {
            result = this._notifications[playerId].FirstOrDefault();
            return result is not null;
        }
    }
}
