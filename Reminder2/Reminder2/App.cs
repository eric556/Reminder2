using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reminder2.Pages;

using Xamarin.Forms;

namespace Reminder2
{
    public class App : Application
    {
        public HomePage homePage = new HomePage();

        public App()
        {
            // The root page of your application
            var content = homePage;
            
            MainPage = new NavigationPage(content);
            MainPage.Title = "Reminders";
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            HomePage.dataAccess.SaveAllReminders();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
