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
using Xamarin.Android;

[assembly: Xamarin.Forms.Dependency(typeof(Notification_Android))]
namespace Reminder2.Droid.Notifications
{
    public class Notification_Android : INotification
    {
        public void deleteNotification(ReminderDataStructure r)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReciver));
            alarmIntent.PutExtra("message", r.Description);
            alarmIntent.PutExtra("title", r.Title);
            alarmIntent.PutExtra("id", r.Id);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, r.Id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

            alarmManager.Cancel(pendingIntent);
        }

        public void sendNotification(ReminderDataStructure r)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReciver));
            alarmIntent.PutExtra("message", r.Description);
            alarmIntent.PutExtra("title", r.Title);
            alarmIntent.PutExtra("id", r.Id);
            var localAlarmTime = r.Time;
            var utcAlarmTime = TimeZoneInfo.ConvertTimeToUtc(localAlarmTime);
            var t = new DateTime(1970, 1, 1) - DateTime.MinValue;
            var epochDifferenceInSeconds = t.TotalSeconds;

            var utcAlarmTimeInMillis = utcAlarmTime.AddSeconds(-epochDifferenceInSeconds).Ticks / 10000;

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, r.Id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

            alarmManager.Set(AlarmType.RtcWakeup, utcAlarmTimeInMillis, pendingIntent);
        }

        public void updateNotification(ReminderDataStructure r)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReciver));
            alarmIntent.PutExtra("message", r.Description);
            alarmIntent.PutExtra("title", r.Title);
            alarmIntent.PutExtra("id", r.Id);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, r.Id, alarmIntent, PendingIntentFlags.UpdateCurrent);
        }
    }
}