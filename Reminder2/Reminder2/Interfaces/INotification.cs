using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder2.Interfaces
{
    public interface INotification
    {
        void sendNotification(string message, Reminder2.Data.ReminderDataStructure r);
    }
}
