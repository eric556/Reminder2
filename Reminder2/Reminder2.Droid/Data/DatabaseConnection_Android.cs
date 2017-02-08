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
using SQLite;
using System.IO;
using Reminder2.Droid.Data;
using Reminder2.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace Reminder2.Droid.Data
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "RemindersDb.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),dbName);
            return new SQLiteConnection(path);
        }
    }
}