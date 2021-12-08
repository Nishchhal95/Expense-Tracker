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
        public static Expense CurrentSelectedItem = null;
        private Dictionary<string, ExpenseType> checkBoxNameToExpenseTypeDictionary = 
            new Dictionary<string, ExpenseType>()
        {
                {"FoodCheckBox", ExpenseType.Food },
                {"ServiceCheckBox", ExpenseType.Service },
                {"TravelCheckBox", ExpenseType.Travel }
        };
        private HashSet<ExpenseType> expenseTypes = new HashSet<ExpenseType>();
        private static Dictionary<string, Func<Expense, IComparable>> nameToPropertyDictionary = new Dictionary<string, Func<Expense, IComparable>>()
        {
            {"Amount", x => x.amount } ,
            {"Description", x => x.description } ,
            {"Date", x => x.expenseDate }
        };
        private string filterText, sortByText = "Date";
        public P_HomePage()
        {
            InitializeComponent();
            InitializeApplication();
        }
        private void InitializeApplication()
        {
            //Read and Update Expenses List
            ExpenseManager.AddExpenses(StorageController.Instance.GetExpenses());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ResetFilters();
            InitTopView();
            CalculateAndUpdateTopView();
            InitializeCollectionViewAndUpdateSelection();
        }

        private void InitTopView()
        {
            MonthlyBudgetCurrencyLabel.Text = StorageController.Instance.GetAppCurrency().CurrencySign;
            MonthlyBudgetLabel.Text = StorageController.Instance.GetMonthlyBudget().ToString();
        }

        private void CalculateAndUpdateTopView()
        {
            float totalExpenses = ExpenseManager.Expenses.Sum(x => x.amount);
            AmountSpentCurrencyLabel.Text = StorageController.Instance.GetAppCurrency().CurrencySign;
            AmountSpentLabel.Text = totalExpenses.ToString();
        }

        private void ResetFilters()
        {
            ExpensesFilter.Text = string.Empty;
            FoodCheckBox.IsChecked = false;
            ServiceCheckBox.IsChecked = false;
            TravelCheckBox.IsChecked = false;
            SortFilterPicker.SelectedIndex = 0;
        }

        private void InitializeCollectionViewAndUpdateSelection()
        {
            List<Expense> sortedExpenseList = new List<Expense>();
            List<Expense> originalExpenseList = ExpenseManager.Expenses.ToList();
            sortedExpenseList = originalExpenseList.OrderBy(nameToPropertyDictionary[sortByText]).ToList();
            ObservableCollection<Expense> sortedObservableCollection = new ObservableCollection<Expense>(sortedExpenseList);
            HomeExpensesCollectionView.ItemsSource = sortedObservableCollection;

            SortFilterPicker.SelectedIndex = 0;

            int currentSelectedItemIndex = 0;

            if (CurrentSelectedItem == null)
            {
                if (sortedObservableCollection == null || sortedObservableCollection.Count == 0)
                {
                    return;
                }
            }
            else
            {
                currentSelectedItemIndex = sortedObservableCollection.IndexOf(CurrentSelectedItem);
            }

            HomeExpensesCollectionView.SelectedItem = null;
            HomeExpensesCollectionView.ScrollTo(sortedObservableCollection[currentSelectedItemIndex]);
        }

        private void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            OpenAddExpensePageAsync();
        }

        private async void OpenAddExpensePageAsync()
        {
            await Navigation.PushAsync(new P_ExpenseDetailsPage());
        }

        private void ClearPreferencesButton_Clicked(object sender, EventArgs e)
        {
            PreferenceController.ClearAllData();
        }

        private void AdvancedSearchSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            AdvancedSearchCheckBoxesLayout.IsVisible = e.Value;
            if (e.Value)
            {
                FilterResultsAndUpdateUI();
            }
        }

        //UI Functions
        private void ExpensesFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterText = e.NewTextValue;
            FilterResultsAndUpdateUI();
        }

        private void ExpenseTypeCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (e.Value)
            {
                expenseTypes.Add(checkBoxNameToExpenseTypeDictionary[checkBox.ClassId]);
            }
            else
            {
                expenseTypes.Remove(checkBoxNameToExpenseTypeDictionary[checkBox.ClassId]);
            }
            if (AdvancedSearchCheckBoxesLayout.IsVisible)
            {
                FilterResultsAndUpdateUI();
            }
        }

        private void SortFilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            sortByText = ((Picker)sender).SelectedItem.ToString();
            FilterResultsAndUpdateUI();
        }

        // NEW FUNCTIONS
        private void FilterResultsAndUpdateUI()
        {
            List<Expense> filteredExpenseList = GetSearchFilterResult(ExpenseManager.Expenses.ToList());
            filteredExpenseList = GetFilterByExpenseType(filteredExpenseList);
            filteredExpenseList = GetFilterBySorting(filteredExpenseList);
            if(filteredExpenseList == null || filteredExpenseList.Count == 0)
            {
                //If we have no Results then show an Empty List.
                filteredExpenseList = new List<Expense>();
            }

            HomeExpensesCollectionView.ItemsSource = filteredExpenseList;
        }

        private List<Expense> GetSearchFilterResult(List<Expense> expenses)
        {
            string localFilterText = string.IsNullOrEmpty(filterText) ? string.Empty : filterText;
            localFilterText = localFilterText.Trim();

            if(expenses == null || expenses.Count == 0)
            {
                return new List<Expense>();
            }
            List<Expense> filteredExpenses = expenses.FindAll(x => x.description.Contains(localFilterText));
            return filteredExpenses;
        }

        private List<Expense> GetFilterByExpenseType(List<Expense> expenses)
        {
            if(expenses == null || expenses.Count == 0)
            {
                return new List<Expense>();
            }

            if(expenseTypes == null || expenseTypes.Count == 0)
            {
                return expenses;
            }

            List<Expense> filteredExpenses = expenses.FindAll((x) =>
            {
                List<ExpenseType> expenseTypesList = expenseTypes.ToList();
                for (int i = 0; i < expenseTypesList.Count; i++)
                {
                    if(x.expenseType == expenseTypesList[i])
                    {
                        return true;
                    }
                }
                return false;
            });

            return filteredExpenses;
        }

        private void HomeExpensesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((Expense)HomeExpensesCollectionView.SelectedItem == null)
            {
                return;
            }
            Navigation.PushAsync(new P_ExpenseDetailsPage((Expense)HomeExpensesCollectionView.SelectedItem));
        }

        private void ItemEditButton_Invoked(object sender, EventArgs e)
        {
            Console.WriteLine(((SwipeItem)sender).Parent.Parent.Parent);
           // Navigation.PushAsync(new P_ExpenseDetailsPage((Expense)HomeExpensesCollectionView.SelectedItem));
        }

        private async void ItemDeleteButton_Invoked(object sender, EventArgs e)
        {
            var alertState = await DisplayAlert("Delete Expense", "Are you sure you want to delete this expense", "Yes", "No");
            if (!alertState)
            {
                return;
            }

            ExpenseManager.RemoveExpense((Expense)HomeExpensesCollectionView.SelectedItem);
            CurrentSelectedItem = null;
            InitializeCollectionViewAndUpdateSelection();
        }

        private List<Expense> GetFilterBySorting(List<Expense> expenses)
        {
            if (expenses == null || expenses.Count == 0)
            {
                return new List<Expense>();
            }

            List<Expense> sortedExpenses = expenses.OrderBy(nameToPropertyDictionary[sortByText]).ToList();
            return sortedExpenses;
        }
    }
}