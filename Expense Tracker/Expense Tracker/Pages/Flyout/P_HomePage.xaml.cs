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
        private ObservableCollection<Model.Expense> expenses = new ObservableCollection<Model.Expense>();
        public ObservableCollection<Model.Expense> Expenses { get { return expenses; } }
        public P_HomePage()
        {
            InitializeComponent();
            UpdateExpenses();
        }

        private void UpdateExpenses()
        {
            HomeExpensesCollectionView.ItemsSource = expenses;
            GetExpenses();
        }

        private void GetExpenses()
        {
            for (int i = 0; i < 2; i++)
            {
                expenses.Add(new Model.Expense(i, (100 * i) + 100, Model.ExpenseType.Service, "Electricity Bill"));
            }
        }
    }
}