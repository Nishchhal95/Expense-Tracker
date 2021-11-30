using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartingPage : ContentPage
    {
        private Dictionary<Button, Layout> buttonToLayoutDictionary = new Dictionary<Button, Layout>();
        public StartingPage()
        {
            InitializeComponent();

            //Fill up Dictionary
            buttonToLayoutDictionary.Add(ExpensesButton, Expenses_Page);
            buttonToLayoutDictionary.Add(RemindersButton, Reminders_Page);

            //Default Page to show is Expenses
            EnablePageLinkedWithButton(ExpensesButton, null);

            //Adding up DummyData
            List<Expense> dummyExpenses = new List<Expense>();

            for (int i = 0; i < 10; i++)
            {
                dummyExpenses.Add(new Expense(i, "Expense " + i, 100 * i + 100, ExpenseType.Food));
            }

            ExpenseCollectionView.ItemsSource = dummyExpenses;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void EnablePageLinkedWithButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            foreach (var btn in buttonToLayoutDictionary.Keys)
            {
                bool isSelectedButton = btn == button;
                buttonToLayoutDictionary[btn].IsVisible = isSelectedButton;
                Color buttonColor = isSelectedButton ? (Color)Application.Current.Resources["SelectedButtonColor"]
                    : (Color)Application.Current.Resources["UnSelectedButtonColor"];
                btn.BackgroundColor = buttonColor;
            }
        }
    }
}