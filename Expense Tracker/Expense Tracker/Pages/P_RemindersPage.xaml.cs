using Expense_Tracker.Controllers;
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
    public partial class P_RemindersPage : ContentPage
    {
        public P_RemindersPage()
        {
            InitializeComponent();
            RemindersCollectionView.ItemsSource = ReminderManager.Reminders;
        }
    }
}