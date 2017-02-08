using System;
using System.Collections.Generic;
using System.Text;
using Reminder2.iOS.Data;
using Reminder2.Interfaces;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]
namespace Reminder2.iOS.Data
{
    public class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "RemindersDb.db3";
            string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}
