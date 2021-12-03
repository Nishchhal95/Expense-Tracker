using Expense_Tracker.Controllers;
using Expense_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker.Pages.Flyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class P_HomePage : ContentPage
    {
        public P_HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            HomeExpensesCollectionView.ItemsSource = ExpenseManager.Expenses;
        }

        private void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            OpenAddExpensePageAsync();
        }

        private void OpenAddExpensePageAsync()
        {
            //await Navigation.PushAsync(new P_AddExpensePage());
            //TODO : For the time being we just directly add a dummy expense
            Expense dummyExpense = new Expense(GetRandomNumberWithinRange(50, 5000),
                (ExpenseType)GetRandomNumberWithinRange(0, Enum.GetValues(typeof(ExpenseType)).Length),
                "Demo Description");
            ExpenseManager.AddExpense(dummyExpense);

            HomeExpensesCollectionView.SelectedItem = dummyExpense;
            HomeExpensesCollectionView.ScrollTo(dummyExpense);
        }

        private int GetRandomNumberWithinRange(int starting, int end)
        {
            Random r = new Random();
            return r.Next(starting, end);
        }

        private void ViewAllExpensesButton_Clicked(object sender, EventArgs e)
        {
            OpenViewAllExpensesPageAsync();
        }

        private async Task OpenViewAllExpensesPageAsync()
        {
            await Navigation.PushAsync(new P_ViewAllExpensesPage());
        }
    }
}