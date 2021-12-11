using Expense_Tracker.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Expense_Tracker.Controllers
{
    public class StorageController
    {
        private const string EXPENSE_LIST_KEY = "ExpenseList";
        private const string EXPENSELIMIT_LIST_KEY = "ExpenseLimitList";
        //private const string REMINDERS_LIST_KEY = "RemindersList";
        private const string MONTHLY_BUDGET_KEY = "MonthlyBudget";
        private const string APP_CURRENCY_KEY = "AppCurrency";
        public static StorageController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StorageController();
                }
                return _instance;
            }
        }
        private static StorageController _instance;

        private StorageController()
        {

        }

        public void FetchSavedExpenseData()
        {
            string jsonToLoad = Preferences.Get(EXPENSE_LIST_KEY, JsonConvert.SerializeObject(null));
        }

        public List<Expense> GetExpenses()
        {
            string jsonToLoad = Preferences.Get(EXPENSE_LIST_KEY, JsonConvert.SerializeObject(new List<Expense>()));
            return JsonConvert.DeserializeObject<List<Expense>>(jsonToLoad);
        }

        public void SetExpenses(List<Expense> expenses)
        {
            Preferences.Set(EXPENSE_LIST_KEY, JsonConvert.SerializeObject(expenses));
        }

        public bool IsFirstTimeLogin()
        {
            string jsonToLoad = Preferences.Get(EXPENSE_LIST_KEY, string.Empty);
            if (!string.IsNullOrEmpty(jsonToLoad) && JsonConvert.DeserializeObject<List<Expense>>(jsonToLoad).Count > 0)
            {
                return false;
            }
            return true;
        }

        public float GetMonthlyBudget()
        {
            float monthlyBudget = Preferences.Get(MONTHLY_BUDGET_KEY, 0f);
            return monthlyBudget;
        }

        public void SetMonthlyBudget(float value)
        {
            Preferences.Set(MONTHLY_BUDGET_KEY, value);
        }

        public AppCurrency GetAppCurrency()
        {
            string json = Preferences.Get(APP_CURRENCY_KEY, JsonConvert.SerializeObject(new AppCurrency("Euro", "€")));
            AppCurrency appCurrency = JsonConvert.DeserializeObject<AppCurrency>(json);
            return appCurrency;
        }

        public void SetAppCurrency(AppCurrency value)
        {
            Preferences.Set(APP_CURRENCY_KEY, JsonConvert.SerializeObject(value));
        }

        public List<ExpenseLimit> GetExpenseLimitList()
        {
            string jsonToLoad = Preferences.Get(EXPENSELIMIT_LIST_KEY, JsonConvert.SerializeObject(new List<ExpenseLimit>()));
            return JsonConvert.DeserializeObject<List<ExpenseLimit>>(jsonToLoad);
        }

        public void SetExpenseLimitList(List<ExpenseLimit> expenseLimits)
        {
            Preferences.Set(EXPENSELIMIT_LIST_KEY, JsonConvert.SerializeObject(expenseLimits));
        }

        public void ClearAllData()
        {
            Preferences.Clear();
        }
    }
}
