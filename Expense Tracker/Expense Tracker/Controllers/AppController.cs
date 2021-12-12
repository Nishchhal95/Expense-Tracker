using Expense_Tracker.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;

namespace Expense_Tracker.Controllers
{
    public class AppController
    {
        public static Dictionary<AppCurrencies, AppCurrency> currencyToStringDictionary = new Dictionary<AppCurrencies, AppCurrency> 
        {
            {AppCurrencies.Euro, new AppCurrency("Euro", "€") },
            {AppCurrencies.INR, new AppCurrency("Indian Rupees", "₹") },
            {AppCurrencies.USD, new AppCurrency("US Dollar", "$") },
            {AppCurrencies.CAD, new AppCurrency("Canadian Dollar", "CA$") }
        };

        private static Dictionary<string, string> localizedResources = new Dictionary<string, string>();

        public static AppController Instance 
        { 
            get
            {
                if(_instance == null)
                {
                    _instance = new AppController();
                }
                return _instance;
            } 
        }
        private static AppController _instance;

        //Expense Realted Stuff

        public AppCurrency SelectedAppCurrency
        {
            get
            {
                return StorageController.Instance.GetAppCurrency();
            }

            set
            {
                StorageController.Instance.SetAppCurrency(value);
            }
        }
        public float MonthlyBudget 
        {
            get
            {
                return StorageController.Instance.GetMonthlyBudget();
            }

            set
            {
                StorageController.Instance.SetMonthlyBudget(value);
            }
        }
        public float AmountSpent { get; set; }
        public float RemainingAmount { get; set; }
        public List<Expense> expenses = new List<Expense>();

        private AppController()
        {

        }

        public static List<string> GetListOfCurrencies()
        {
            List<string> currencyNames = new List<string>();
            foreach (var item in currencyToStringDictionary.Values)
            {
                currencyNames.Add(item.CurrencyName);
            }
            return currencyNames;
        }

        public static Dictionary<string, string> GetLocalizedResources()
        {
            if(localizedResources != null && localizedResources.Count > 0)
            {
                return localizedResources;
            }
            ResourceManager MyResourceClass = new ResourceManager(typeof(MyResources));

            ResourceSet resourceSet = MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key.ToString();
                object resource = entry.Value;

                localizedResources.Add(resourceKey, resource.ToString());
            }

            return localizedResources;
        }

        public void SaveData()
        {
            StorageController.Instance.SetExpenses(ExpenseManager.Expenses.ToList());
        }
    }

    public enum AppCurrencies
    {
        Euro,
        INR,
        USD,
        CAD
    }

    public class AppCurrency
    {
        public AppCurrency(string currencyName, string currencySign)
        {
            CurrencyName = currencyName;
            CurrencySign = currencySign;
        }

        public string CurrencyName { get; set; }
        public string CurrencySign { get; set; }
    }
}
