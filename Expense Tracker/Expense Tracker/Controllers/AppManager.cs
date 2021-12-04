using Expense_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Expense_Tracker.Controllers
{

    public static class AppManager
    {
        public static void Initialize()
        {
            //TODO : Fetch Data From Somewhere
            if (!LoadAllData())
            {
                ExpenseManager.CreateDummyData();
            }
        }

        public static void SaveAllData()
        {
            //TODO : Add Saving for Reminders too
            PreferenceController.Save(ListTypes.Expenses, ExpenseManager.Expenses);
        }

        public static bool LoadAllData()
        {
            //TODO : Add Loading for Reminders too
            ObservableCollection<Expense> expenses = PreferenceController.LoadData<Expense>(ListTypes.Expenses);
            if(expenses == null || expenses.Count == 0)
            {
                return false;
            }
            ExpenseManager.AddExpenses(expenses);
            return true;
        }
    }
}
