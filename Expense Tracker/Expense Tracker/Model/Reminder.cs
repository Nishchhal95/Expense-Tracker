using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Model
{
    public class Reminder
    {
        public Reminder(string reminderName, string description, DateTime date, DateTime time, RepeatMode repeatMode)
        {
            _id++;
            id = _id;
            this.reminderName = reminderName;
            this.description = description;
            this.date = date;
            this.time = time;
            this.repeatMode = repeatMode;
        }

        public static int _id = -1;
        public int id { get; set; }
        public string reminderName { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public RepeatMode repeatMode { get; set; }
    }

    public enum RepeatMode
    {
        NoRepeat,
        EveryDay,
        EveryWeek,
        EveryMonth,
        EveryThreeMonths,
        EveryYear
    }
}
