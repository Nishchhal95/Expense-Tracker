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
    public partial class P_ExpenseDetailsPage : ContentPage
    {
        private Expense expense;
        public P_ExpenseDetailsPage()
        {
            InitializeComponent();
            this.expense = null;
        }

        public P_ExpenseDetailsPage(Expense expense)
        {
            InitializeComponent();
            this.expense = expense;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeUI();
        }

        private void InitializeUI()
        {
            List<string> expenseTypeStringsList = Enum.GetNames(typeof(ExpenseType)).ToList();
            ExpenseTypePicker.ItemsSource = expenseTypeStringsList;
            ExpenseTypePicker.SelectedIndex = 0;

            if (expense == null)
            {
                SetupAsAddPage();
            }

            else
            {
                SetupAsUpdatePage();
            }
        }

        private void SetupAsAddPage()
        {
            ExpenseDetailsUI.IsEnabled = true;
            UpdateExpenseButtonUI.IsVisible = false;
            AddExpenseButtonUI.IsVisible = true;
        }

        private void SetupAsUpdatePage()
        {
            ExpenseDetailsUI.IsEnabled = false;
            UpdateExpenseButtonUI.IsVisible = true;
            AddExpenseButtonUI.IsVisible = false;

            //Populate UI
            if(expense == null)
            {
                return;
            }

            ExpenseAmount.Text = expense.amount.ToString();
            ExpenseTypePicker.SelectedIndex = (int)expense.expenseType;
            ExpenseDatePicker.Date = expense.expenseDate;
            ExpenseDescription.Text = expense.description;
        }

        private void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            ReadDataFromUI((float amount, ExpenseType expenseType, DateTime expenseDate, string expenseDescription) =>
            {
                //Actually creating and adding an expense.
                Expense expense = new Expense(amount, expenseType, expenseDescription, expenseDate);
                ExpenseManager.AddExpense(expense);
            });
        }

        private void EditExpenseButton_Clicked(object sender, EventArgs e)
        {
            ExpenseDetailsUI.IsEnabled = true;
        }

        private void UpdateExpenseButton_Clicked(object sender, EventArgs e)
        {
            //Update the expense in the collection
            ReadDataFromUI((float amount, ExpenseType expenseType, DateTime expenseDate, string expenseDescription) =>
            {
                expense.amount = amount;
                expense.expenseType = expenseType;
                expense.expenseDate = expenseDate;
                expense.description = expenseDescription;
            });
            
        }

        private void ReadDataFromUI(Action<float, ExpenseType, DateTime, string> action)
        {
            //Add Expense to the collection
            string amountString = ExpenseAmount.Text;
            if (string.IsNullOrEmpty(amountString))
            {
                amountString = string.Empty;
            }

            amountString = amountString.Trim();
            if (!float.TryParse(amountString, out float amount))
            {
                //Display Alert and return;
                DisplayAlert("Alert!", "Please Enter Amount!", "Ok");
                return;
            }

            ExpenseType expenseType = (ExpenseType)ExpenseTypePicker.SelectedIndex;
            DateTime expenseDate = ExpenseDatePicker.Date;
            string expenseDescription = ExpenseDescription.Text;

            if (string.IsNullOrEmpty(expenseDescription))
            {
                expenseDescription = string.Empty;
            }

            action?.Invoke(amount, expenseType, expenseDate, expenseDescription);

            P_HomePage.CurrentSelectedItem = expense;
            Navigation.PopAsync();
        }
    }
}