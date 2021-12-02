using Expense_Tracker.Pages;
using Expense_Tracker.Pages.Flyout;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker
{
    public partial class App : Application
    {
        public static ObservableCollection<Model.Expense> Expenses = new ObservableCollection<Model.Expense>();
        public App()
        {
            InitializeComponent();
            MainPage = new ET_Flyout();
            GetExpenses();
        }

        private void GetExpenses()
        {
            for (int i = 0; i < 2; i++)
            {
                App.Expenses.Add(new Model.Expense(i, (100 * i) + 100, Model.ExpenseType.Service, "Electricity Bill"));
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
