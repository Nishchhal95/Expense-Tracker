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
        private HashSet<ExpenseType> expenseTypes = new HashSet<ExpenseType>();
        private string filterText, sortByText;
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
                "Demo Description", DateTime.Now);
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

        private void ClearPreferencesButton_Clicked(object sender, EventArgs e)
        {
            PreferenceController.ClearAllData();
        }

        private void AdvancedSearchSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            AdvancedSearchCheckBoxesLayout.IsVisible = e.Value;
        }

        private void FoodCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                expenseTypes.Add(ExpenseType.Food);
            }
            else
            {
                expenseTypes.Remove(ExpenseType.Food);
            }
            FilterResultBasedOnExpenseType();
        }

        private void ServiceCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                expenseTypes.Add(ExpenseType.Service);
            }
            else
            {
                expenseTypes.Remove(ExpenseType.Service);
            }
            FilterResultBasedOnExpenseType();
        }

        private void TravelCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                expenseTypes.Add(ExpenseType.Travel);
            }
            else
            {
                expenseTypes.Remove(ExpenseType.Travel);
            }
            FilterResultBasedOnExpenseType();
        }

        private void ExpensesFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterText = e.NewTextValue;
            FilterResultBasedOnExpenseType();
        }

        private void SortFilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            sortByText = picker.SelectedItem.ToString();
            FilterResultBasedOnExpenseType();
        }

        private void FilterResultBasedOnExpenseType()
        {
            if (!AdvancedSearchCheckBoxesLayout.IsVisible)
            {
                return;
            }

            Console.WriteLine("****Filtering Results " + filterText + " -- " + sortByText + " -- " + expenseTypes.Count);
            List<Expense> filterResult = ExpenseManager.GetExpensesOnCustomFilter<Expense>(filterText, sortByText, expenseTypes.ToList());
            HomeExpensesCollectionView.ItemsSource = filterResult;
        }
    }
}