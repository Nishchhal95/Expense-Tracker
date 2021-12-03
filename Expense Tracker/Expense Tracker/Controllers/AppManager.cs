using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Controllers
{
    public static class AppManager
    {
        public static void Initialize()
        {
            //TODO : Fetch Data From Somewhere

            //Create Dummy Data For now
            ExpenseManager.CreateDummyData();
        }
    }
}
