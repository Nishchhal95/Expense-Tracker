using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace Expense_Tracker.Controllers
{
    public static class PreferenceController
    {
        private const string EXPENSE_LIST_KEY = "ExpenseList";
        private const string REMINDERS_LIST_KEY = "RemindersList";
        private static readonly Dictionary<ListTypes, string> listTypesToKeyDictionary = new Dictionary<ListTypes, string>()
        {
            {ListTypes.Expenses, EXPENSE_LIST_KEY },
            {ListTypes.Reminders, REMINDERS_LIST_KEY }
        };

        public static void Initialize()
        {

        }

        public static void Save<T>(ListTypes listType, ReadOnlyObservableCollection<T> listToSave)
        {
            try
            {
                string jsonToSave = JsonConvert.SerializeObject(listToSave);
                if (!listTypesToKeyDictionary.TryGetValue(listType, out string listToSaveKey))
                {
                    Console.WriteLine("****Key not found");
                    return;
                }
                Preferences.Set(listToSaveKey, jsonToSave);
            }
            catch(Exception e)
            {
                Console.WriteLine($"****{e.Message}");
            }
            
        }

        public static ObservableCollection<T> LoadData<T>(ListTypes listType)
        {
            try
            {
                if (!listTypesToKeyDictionary.TryGetValue(listType, out string listToLoadKey))
                {
                    Console.WriteLine("****Key not found");
                    return null;
                }
                string jsonToLoad = Preferences.Get(listToLoadKey, JsonConvert.SerializeObject(null));
                return JsonConvert.DeserializeObject<ObservableCollection<T>>(jsonToLoad);
            }
            catch(Exception e)
            {
                Console.WriteLine($"****{e.Message}");
                return null;
            }
        }

        public static void ClearAllData()
        {
            ExpenseManager.RemoveAll();
            Preferences.Clear();
        }

        public static void ClearDataForKey(ListTypes listType)
        {
            if (!listTypesToKeyDictionary.TryGetValue(listType, out string preferenceKey))
            {
                Console.WriteLine("****Key not found");
                return;
            }
            Preferences.Remove(preferenceKey);
        }
    }
}

public enum ListTypes
{
    Expenses,
    Reminders
}
