using Expense_Tracker.Controllers;
using Expense_Tracker.Model;
using Expense_Tracker.Pages.Flyout;
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
    public partial class P_AppSetupPage : ContentPage
    {
        public P_AppSetupPage()
        {
            InitializeComponent();
            List<ExpenseLimit> expenseLimits = StorageController.Instance.GetExpenseLimitList();
            if (expenseLimits == null || expenseLimits.Count == 0)
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CurrencyPicker.ItemsSource = AppController.GetListOfCurrencies();
            CurrencyPicker.SelectedIndex = 0;


            ConfigureExpensesCollectionView.ItemsSource = ExpenseManager.ExpensesLimits;
        }

        private void ProceedButton_Clicked(object sender, EventArgs e)
        {
            string monthlyBudgetString = MonthlyBudgetInputField.Text;
            if (string.IsNullOrEmpty(monthlyBudgetString))
            {
                DisplayAlert("Error!", "Please fill Monthly Budget to proceed!", "Ok");
                return;
            }

            if(!float.TryParse(monthlyBudgetString, out float monthlyBudget))
            {
                DisplayAlert("Error!", "Please use numbers to fill Monthly Budget!", "Ok");
                return;
            }

            float expenseLimitTotal = 0;
            foreach (var item in ExpenseManager.ExpensesLimits)
            {
                expenseLimitTotal += item.ExpenseMaxLimit;
            }

            if (expenseLimitTotal > monthlyBudget)
            {
                DisplayAlert("Error!", "Your Configuration Exceeds the Monthly Budget", "Ok");
                return;
            }

            StorageController.Instance.SetExpenseLimitList(ExpenseManager.ExpensesLimits.ToList());

            //Save Monthly Budget
            AppController.Instance.MonthlyBudget = monthlyBudget;
            //Save Selected Currency
            AppController.Instance.SelectedAppCurrency = AppController.currencyToStringDictionary[(AppCurrencies)CurrencyPicker.SelectedIndex];

            //Bypass Navigation to make this as Root Page
            Page page = new ET_Flyout();
            NavigationPage.SetHasBackButton(page, false);
            NavigationPage.SetHasNavigationBar(page, false);
            Application.Current.MainPage = page;
        }

        private void CurrencyPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppCurrency currentSelectedAppCurrency = AppController.currencyToStringDictionary[(AppCurrencies)CurrencyPicker.SelectedIndex];
            string currencySymbol = currentSelectedAppCurrency.CurrencySign;
            CurrencySignLabel.Text = currencySymbol;

            foreach (var item in ExpenseManager.ExpensesLimits)
            {
                item.CurrencySign = currencySymbol;
            }
        }
    }
}