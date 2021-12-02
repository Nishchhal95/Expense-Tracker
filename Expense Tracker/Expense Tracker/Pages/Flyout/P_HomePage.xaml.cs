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
            UpdateExpenses();
        }

        private void UpdateExpenses()
        {
            HomeExpensesCollectionView.ItemsSource = App.Expenses;
        }

        private void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            OpenAddExpensePageAsync();
        }

        private void OpenAddExpensePageAsync()
        {
            //await Navigation.PushAsync(new P_AddExpensePage());
            //TODO : For the time being we just directly add a dummy expense
            App.Expenses.Add(new Model.Expense(App.Expenses.Count, 
                GetRandomNumberWithinRange(50, 5000), 
                (Model.ExpenseType)GetRandomNumberWithinRange(0, Enum.GetValues(typeof(Model.ExpenseType)).Length), 
                "Demo Description"));

            HomeExpensesCollectionView.SelectedItem = App.Expenses[App.Expenses.Count - 1];
            HomeExpensesCollectionView.ScrollTo(App.Expenses[App.Expenses.Count - 1]);
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