using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Reminder2.Data;
using System.Globalization;

namespace Reminder2.Pages
{
    public class ReminderCreationPage : ContentPage
    {

        Entry titleEntry = new Entry
        {
            Placeholder = "Title",
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        Editor descriptionEntry = new Editor()
        {
            VerticalOptions = LayoutOptions.FillAndExpand
        };

        DatePicker date = new DatePicker();

        TimePicker time = new TimePicker();


        public ReminderCreationPage(string title, string description, DateTime dTime)
        {
            
            StackLayout mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            Title = "New Reminder";
            var titleLabel = new Label
            {
                Text = "Title: "
            };
            var descriptionLabel = new Label
            {
                Text = "Description:"
            };



            StackLayout descriptonLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            descriptonLayout.Children.Add(descriptionLabel);
            descriptonLayout.Children.Add(descriptionEntry);

            Button addButton = new Button()
            {
                Text = "Add"
            };

            addButton.Clicked += onAddButton;

            mainLayout.Children.Add(titleEntry);
            mainLayout.Children.Add(descriptonLayout);
            mainLayout.Children.Add(date);
            mainLayout.Children.Add(time);
            mainLayout.Children.Add(addButton);

            titleEntry.Text = title;
            descriptionEntry.Text = description;
            date.Date = dTime.Date;
            time.Time = dTime.TimeOfDay;

            Content = mainLayout;
        }

        public ReminderCreationPage():this("","", DateTime.Now)
        {
        }

        private async void onAddButton(Object sender, EventArgs e)
        {
            if (titleEntry.Text != null)
            {
                Match match = Regex.Match(titleEntry.Text, @"^\\s+$", RegexOptions.None);
                if (!match.Success && !titleEntry.Text.Equals(""))
                {
                    ReminderDataStructure r = new ReminderDataStructure(titleEntry.Text, descriptionEntry.Text, date.Date.Add(time.Time));
                    HomePage.notificationManager.sendNotification(r);
                    HomePage.dataAccess.Reminders.Add(r);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}
