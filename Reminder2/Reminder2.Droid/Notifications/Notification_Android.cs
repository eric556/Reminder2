using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Reminder2.Droid.Notifications;
using Reminder2.Interfaces;
using Reminder2.Data;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Notification_Android))]
namespace Reminder2.Droid.Notifications
{
    public class Notification_Android : INotification
    {
        public void sendNotification(string message, ReminderDataStructure r)
        {
            Notification.Builder builder = new Notification.Builder(Forms.Context);
            builder.SetContentTitle("Reminder").SetContentText(message).SetSmallIcon(Resource.Drawable.icon);
            Notification notification = builder.Build();
            
            NotificationManager manager = Forms.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            manager.Notify(r.Id, notification);
        }
    }
}