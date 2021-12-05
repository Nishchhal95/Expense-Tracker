using Expense_Tracker.Pages.Flyout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Expense_Tracker.Controllers;
using Expense_Tracker.Model;

namespace Expense_Tracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class P_AddExpensePage : ContentPage
    {
        public P_AddExpensePage()
        {
            InitializeComponent();
        }

        private void AddExpenseButton_Clicked(object sender, EventArgs e)
        {
            string amount = AmountField.Text.Trim();
            Model.ExpenseType expenseType = (Model.ExpenseType)ExpenseTypePicker.SelectedIndex;
            string description = DescriptionField.Text.Trim();

            if (string.IsNullOrEmpty(amount) || !float.TryParse(amount, out float realAmount) || ExpenseDatePicker.Date == null)
            {
                DisplayAlert("Alert", "Please Enter Amount or Select a Date", "Cancel");
                return;
            }

            Expense expense = new Expense(realAmount, expenseType, description, ExpenseDatePicker.Date);
            P_HomePage.CurrentSelectedItem = expense;
            AddExpenseAndMoveToViewAllExpensesPageAsync(expense);
        }

        private async Task AddExpenseAndMoveToViewAllExpensesPageAsync(Expense expense)
        {
            ExpenseManager.AddExpense(expense);
            await Navigation.PopAsync();
        }
    }
}