using Expense_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Expense_Tracker.Controllers
{
    public static class ReminderManager
    {
        private static ObservableCollection<Reminder> reminders = new ObservableCollection<Reminder>();
        public static ReadOnlyObservableCollection<Reminder> Reminders { get { return new ReadOnlyObservableCollection<Reminder>(reminders); } }

        public static void AddReminder(Reminder reminder)
        {
            reminders.Add(reminder);
        }

        public static void RemoveReminder(Reminder reminder)
        {
            reminders.Remove(reminder);
        }

        public static void RemoveReminder(int reminderId)
        {
            reminders.Remove(reminders.First(x => x.id == reminderId));
        }

        public static void RemoveAll()
        {
            reminders = new ObservableCollection<Reminder>();
        }

        public static Reminder GetReminder(int reminderId)
        {
            return reminders.First(x => x.id == reminderId);
        }

        public static void AddReminders(List<Reminder> reminders)
        {
            ReminderManager.reminders = new ObservableCollection<Reminder>(reminders);
        }

        public static void AddReminders(ObservableCollection<Reminder> reminders)
        {
            ReminderManager.reminders = new ObservableCollection<Reminder>(reminders);
        }

        //Create and Fill Dummy Data
        public static void CreateDummyData()
        {
            //TODO : Just to make sure we dont double up the data 
            Console.WriteLine("****Clearing Reminders Collection");
            reminders = new ObservableCollection<Reminder>();

            for (int i = 0; i < 2; i++)
            {
                Reminder reminder = new Reminder("Test Reminder", "Dummy Description",
                    new DateTime(2021, 12, 5), new DateTime(2021, 12, 5, 12, 10, 0), RepeatMode.NoRepeat);
                reminders.Add(reminder);
            }
        }
    }
}
