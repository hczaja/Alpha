using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Notifications
{
    public interface INotificationService
    {
        bool HasNotificationsFor(int playerId);

        void EnqueueNotification(int playerId, Notification notification);
        void DequeueNotification(int playerId);
        bool TryGetNotification(int playerId, out Notification? result);
    }
}
