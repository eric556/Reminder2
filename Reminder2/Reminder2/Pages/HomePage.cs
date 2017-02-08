using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Reminder2.Data;
using Reminder2.Elements;
using Reminder2.Notifications;

using Xamarin.Forms;



namespace Reminder2.Pages
{
    public class HomePage : ContentPage
    {
        public static RemindersDataAccess dataAccess;
        public static ReminderNotificationManager notificationManager;
        public HomePage()
        {
            dataAccess = new RemindersDataAccess();
            notificationManager = new ReminderNotificationManager();
            ListView view = new ListView();
            view.ItemsSource = dataAccess.Reminders;
            view.HasUnevenRows = true;
            Button add = new Button(){ Text = "Add"};

            view.ItemTemplate = new DataTemplate(typeof(ReminderCell));

            view.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    DisplayAlert(((ReminderDataStructure)e.SelectedItem).Title, ((ReminderDataStructure)e.SelectedItem).Description, "OK");
                }
                ((ListView)sender).SelectedItem = null;
            };

            add.Clicked += onAddClicked;



            StackLayout layout = new StackLayout();
            layout.Children.Add(view);
            layout.Children.Add(add);
            Content = layout;
        }

        private async void onAddClicked(Object sender, EventArgs e)
        {
            var addPage = new ReminderCreationPage();
            await Navigation.PushModalAsync(addPage);
        }
    }
}
