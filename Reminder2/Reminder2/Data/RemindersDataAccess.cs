using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Reminder2.Interfaces;

namespace Reminder2.Data
{
    public class RemindersDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<ReminderDataStructure> Reminders { get; set; }
        public RemindersDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<ReminderDataStructure>();
            this.Reminders = new ObservableCollection<ReminderDataStructure>(database.Table<ReminderDataStructure>());
            // If the table is empty, initialize the collection
            if (!database.Table<ReminderDataStructure>().Any())
            {
                //AddNewReminder();
            }
        }
        public void AddNewReminder()
        {
            this.Reminders.
              Add(new ReminderDataStructure
              {
                  Title = "Test",
                  Description = "This is a test"
              });
        }
        public ReminderDataStructure GetReminder(int id)
        {
            lock (collisionLock)
            {
                return database.Table<ReminderDataStructure>().
                  FirstOrDefault(ReminderDataStructure => ReminderDataStructure.Id == id);
            }
        }
        public int SaveReminder(ReminderDataStructure reminderDataStructureInstance)
        {
            lock (collisionLock)
            {
                if (reminderDataStructureInstance.Id != 0)
                {
                    database.Update(reminderDataStructureInstance);
                    Reminders.Add(reminderDataStructureInstance);
                    return reminderDataStructureInstance.Id;
                }
                else
                {
                    database.Insert(reminderDataStructureInstance);
                    Reminders.Add(reminderDataStructureInstance);
                    return reminderDataStructureInstance.Id;
                }
            }
        }
        public void SaveAllReminders()
        {
            lock (collisionLock)
            {
                foreach (var reminderDataStructureInstance in this.Reminders)
                {
                    if (reminderDataStructureInstance.Id != 0)
                    {
                        database.Update(reminderDataStructureInstance);
                    }
                    else
                    {
                        database.Insert(reminderDataStructureInstance);
                    }
                }
            }
        }
        public int DeleteReminder(ReminderDataStructure reminderDataStructureInstance)
        {
            var id = reminderDataStructureInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<ReminderDataStructure>(id);
                }
            }
            this.Reminders.Remove(reminderDataStructureInstance);
            return id;
        }
        public void DeleteAllReminders()
        {
            lock (collisionLock)
            {
                database.DropTable<ReminderDataStructure>();
                database.CreateTable<ReminderDataStructure>();
            }
            this.Reminders = null;
            this.Reminders = new ObservableCollection<ReminderDataStructure>
              (database.Table<ReminderDataStructure>());
        }
    }
}