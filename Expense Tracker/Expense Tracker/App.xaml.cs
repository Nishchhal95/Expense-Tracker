using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new StartingPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
