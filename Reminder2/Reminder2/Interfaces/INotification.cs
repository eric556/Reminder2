using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder2.Interfaces
{
    public interface INotification
    {
        void sendNotification(Reminder2.Data.ReminderDataStructure r);
        void updateNotification(Reminder2.Data.ReminderDataStructure r);
        void deleteNotification(Reminder2.Data.ReminderDataStructure r);
    }
}
