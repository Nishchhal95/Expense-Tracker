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

            List<string> appLanguages = AppController.appLanguageDictionary.Values.ToList();
            LanguagePicker.ItemsSource = appLanguages;
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

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            float monthlyBudget = StorageController.Instance.GetMonthlyBudget();
            float expenseLimitTotal = 0;
            foreach (var item in ExpenseManager.ExpensesLimits)
            {
                expenseLimitTotal += item.ExpenseMaxLimit;
            }

            if(expenseLimitTotal > monthlyBudget)
            {
                DisplayAlert("Error!", "Your Configuration Exceeds the Monthly Budget", "Ok");
                return;
            }

            StorageController.Instance.SetExpenseLimitList(ExpenseManager.ExpensesLimits.ToList());

            DisplayAlert("Success!", "New Limits Updated!", "Ok");
        }
    }
}