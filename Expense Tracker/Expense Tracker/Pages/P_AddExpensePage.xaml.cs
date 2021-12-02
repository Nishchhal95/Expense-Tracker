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

            if (string.IsNullOrEmpty(amount) || !float.TryParse(amount, out float realAmount))
            {
                DisplayAlert("Alert", "Please Enter Amount!", "Cancel");
                return;
            }

            AddExpenseAndMoveToViewAllExpensesPageAsync(realAmount, expenseType, description);
        }

        private async Task AddExpenseAndMoveToViewAllExpensesPageAsync(float realAmount, Model.ExpenseType expenseType, string description)
        {
            App.Expenses.Add(new Model.Expense(App.Expenses.Count, realAmount, expenseType, description));
            await Navigation.PopAsync();
        }
    }
}