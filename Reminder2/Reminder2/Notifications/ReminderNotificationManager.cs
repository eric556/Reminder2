using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Reminder2.Interfaces;

namespace Reminder2.Notifications
{ 
    public class ReminderNotificationManager
    {
        private INotification notificationInterface;

        public ReminderNotificationManager()
        {
            notificationInterface = DependencyService.Get<INotification>();
        }

        public void sendNotification(string message, Reminder2.Data.ReminderDataStructure r)
        {
            notificationInterface.sendNotification(message, r);
        }
    }
}
