using Expense_Tracker.Controllers;
using Expense_Tracker.Pages.Flyout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class P_ViewAllExpensesPage : ContentPage
    {
        public P_ViewAllExpensesPage()
        {
            InitializeComponent();
            ViewAllExpensesCollectionView.ItemsSource = ExpenseManager.Expenses;
        }

        private void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            OpenAddExpensePageAsync();
        }

        private async Task OpenAddExpensePageAsync()
        {
            await Navigation.PushAsync(new P_AddExpensePage());
        }

        private void ViewExpensesFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterValue = e.NewTextValue.Trim();

            if (string.IsNullOrEmpty(filterValue))
            {
                ViewAllExpensesCollectionView.ItemsSource = ExpenseManager.Expenses;
            }

            else
            {
                List<Model.Expense> filterResult = ExpenseManager.Expenses.Where(x => x.description.Contains(filterValue) 
                || x.expenseType.ToString().Contains(filterValue)).ToList();
                ViewAllExpensesCollectionView.ItemsSource = filterResult;
            }
        }
    }
}