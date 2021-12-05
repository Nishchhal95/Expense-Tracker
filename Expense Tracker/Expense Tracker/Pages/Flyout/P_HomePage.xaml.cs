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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ResetFilters();
            InitializeCollectionViewAndUpdateSelection();
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

            HomeExpensesCollectionView.SelectedItem = sortedObservableCollection[currentSelectedItemIndex];
            HomeExpensesCollectionView.ScrollTo(CurrentSelectedItem);
        }

        private void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            OpenAddExpensePageAsync();
        }

        private async void OpenAddExpensePageAsync()
        {
            await Navigation.PushAsync(new P_AddExpensePage());
            //TODO : For the time being we just directly add a dummy expense
            //Expense dummyExpense = new Expense(GetRandomNumberWithinRange(50, 5000),
            //    (ExpenseType)GetRandomNumberWithinRange(0, Enum.GetValues(typeof(ExpenseType)).Length),
            //    "Demo Description", RandomDay());
            //ExpenseManager.AddExpense(dummyExpense);

            //HomeExpensesCollectionView.SelectedItem = dummyExpense;
            //HomeExpensesCollectionView.ScrollTo(dummyExpense);
        }

        private int GetRandomNumberWithinRange(int starting, int end)
        {
            Random r = new Random();
            return r.Next(starting, end);
        }

        private DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
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
            if (e.Value)
            {
                FilterResultsAndUpdateUI();
            }
        }

        //private void FoodCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        //{
        //    if (e.Value)
        //    {
        //        expenseTypes.Add(ExpenseType.Food);
        //    }
        //    else
        //    {
        //        expenseTypes.Remove(ExpenseType.Food);
        //    }
        //    FilterResultBasedOnExpenseType();
        //}

        //private void ServiceCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        //{
        //    if (e.Value)
        //    {
        //        expenseTypes.Add(ExpenseType.Service);
        //    }
        //    else
        //    {
        //        expenseTypes.Remove(ExpenseType.Service);
        //    }
        //    FilterResultBasedOnExpenseType();
        //}

        //private void TravelCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        //{
        //    if (e.Value)
        //    {
        //        expenseTypes.Add(ExpenseType.Travel);
        //    }
        //    else
        //    {
        //        expenseTypes.Remove(ExpenseType.Travel);
        //    }
        //    FilterResultBasedOnExpenseType();
        //}

        //private void ExpensesFilter_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    filterText = e.NewTextValue;
        //    FilterResultBasedOnExpenseType();
        //}

        //private void SortFilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Picker picker = (Picker)sender;
        //    sortByText = picker.SelectedItem.ToString();
        //    FilterResultBasedOnExpenseType();
        //}

        //private void FilterResultBasedOnExpenseType()
        //{
        //    if (!AdvancedSearchCheckBoxesLayout.IsVisible)
        //    {
        //        return;
        //    }

        //    Console.WriteLine("****Filtering Results " + filterText + " -- " + sortByText + " -- " + expenseTypes.Count);
        //    List<Expense> filterResult = ExpenseManager.GetExpensesOnCustomFilter<Expense>(filterText, sortByText, expenseTypes.ToList());
        //    HomeExpensesCollectionView.ItemsSource = filterResult;
        //}

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