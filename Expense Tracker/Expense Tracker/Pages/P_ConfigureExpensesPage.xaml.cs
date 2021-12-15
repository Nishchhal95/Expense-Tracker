using Expense_Tracker.Controllers;
using Expense_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class P_ConfigureExpensesPage : ContentPage
    {   
        public P_ConfigureExpensesPage()
        {
            InitializeComponent();
            List<ExpenseLimit> expenseLimits = StorageController.Instance.GetExpenseLimitList();
            if(expenseLimits == null || expenseLimits.Count == 0)
            {
                for (int i = 0; i < Enum.GetNames(typeof(ExpenseType)).Length; i++)
                {
                    expenseLimits.Add(new ExpenseLimit()
                    {
                        ExpenseMaxLimit = 0,
                        CurrencySign = StorageController.Instance.GetAppCurrency().CurrencySign,
                        ExpenseType = (ExpenseType)i
                    });
                }

                StorageController.Instance.SetExpenseLimitList(expenseLimits);
            }

            ExpenseManager.AddExpenseLimitList(expenseLimits);

            List<string> appLanguages = AppController.appLanguageDictionary.Keys.ToList();
            LanguagePicker.ItemsSource = appLanguages;
            string selectedLanguage = "en";
            string selectedPickerLanguage = "English";
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                selectedLanguage = Application.Current.Properties["Language"].ToString();
            }
            if (string.IsNullOrEmpty(selectedLanguage))
            {
                selectedLanguage = "en";
            }

            foreach (var item in AppController.appLanguageDictionary.Keys)
            {
                if (AppController.appLanguageDictionary[item].Equals(selectedLanguage))
                {
                    selectedPickerLanguage = item;
                    break;
                }
            }

            LanguagePicker.SelectedItem = selectedPickerLanguage;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitPage();
            
        }

        private void InitPage()
        {
            MonthlyBudgetCurrencySignLabel.Text = StorageController.Instance.GetAppCurrency().CurrencySign;
            MonthlyBudgetAmountLabel.Text = StorageController.Instance.GetMonthlyBudget().ToString();
            ConfigureExpensesCollectionView.ItemsSource = ExpenseManager.ExpensesLimits;
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            float monthlyBudget = StorageController.Instance.GetMonthlyBudget();
            float expenseLimitTotal = 0;
            foreach (var item in ExpenseManager.ExpensesLimits)
            {
                expenseLimitTotal += item.ExpenseMaxLimit;
            }

            string selectedLanguage = LanguagePicker.SelectedItem.ToString();
            if(!AppController.appLanguageDictionary.TryGetValue(selectedLanguage, out string selectedLanguageCode)
                || string.IsNullOrEmpty(selectedLanguageCode))
            {
                DisplayAlert("Error!", "Select Language again...", "Ok");
                return;
            }

            if(expenseLimitTotal > monthlyBudget)
            {
                DisplayAlert("Error!", "Your Configuration Exceeds the Monthly Budget", "Ok");
                return;
            }

            StorageController.Instance.SetExpenseLimitList(ExpenseManager.ExpensesLimits.ToList());
            Application.Current.Properties["Language"] = selectedLanguageCode;
            await Application.Current.SavePropertiesAsync();

            DisplayAlert("Success!", "New Limits Updated, Language changes will take affect after a restart of the application.", "Ok");
        }
    }
}