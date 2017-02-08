using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reminder2.Pages;
using Reminder2.Data;
using Xamarin.Forms;

namespace Reminder2.Elements
{
    class ReminderCell : ViewCell
    {
        public ReminderCell()
        {
            StackLayout cellWrapper = new StackLayout();
            StackLayout verticalLayout = new StackLayout();
            Label top = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            Label bottom = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            top.SetBinding(Label.TextProperty, "Title");
            bottom.SetBinding(Label.TextProperty, "Description");

            MenuItem moreAction = new MenuItem { Text = "Edit" };
            moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            moreAction.Clicked += async (sender, e) =>
            {
                var addPage = new ReminderCreationPage(top.Text,bottom.Text);
                MenuItem mi = ((MenuItem)sender);
                ReminderDataStructure r = ((ReminderDataStructure)mi.CommandParameter);
                HomePage.dataAccess.DeleteReminder(r);
                await App.Current.MainPage.Navigation.PushModalAsync(addPage);
            };

            MenuItem deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked +=  (sender, e) =>
            {
                MenuItem mi = ((MenuItem)sender);
                ReminderDataStructure r = ((ReminderDataStructure)mi.CommandParameter);
                HomePage.dataAccess.DeleteReminder(r);
            };
            ContextActions.Add(moreAction);
            ContextActions.Add(deleteAction);

            verticalLayout.Orientation = StackOrientation.Vertical;
            verticalLayout.Children.Add(top);
            verticalLayout.Children.Add(bottom);
            cellWrapper.Children.Add(verticalLayout);
            View = cellWrapper;
        }
    }
}
