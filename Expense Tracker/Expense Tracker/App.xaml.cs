using Expense_Tracker.Controllers;
using Expense_Tracker.Pages;
using Expense_Tracker.Pages.Flyout;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //My Method
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            UpdateUserLanguageSettings();
            //Check if this is a first time user
            if (StorageController.Instance.IsFirstTimeLogin())
            {
                MainPage = new NavigationPage(new P_AppSetupPage());
            }
            else
            {
                MainPage = new ET_Flyout();
            }

            
        }

        private void UpdateUserLanguageSettings()
        {
            string currentLanguage = "en";
            if (Current.Properties.ContainsKey("Language"))
            {
                currentLanguage = Current.Properties["Language"].ToString();
            }

            CultureInfo.CurrentCulture = new CultureInfo(currentLanguage);
            CultureInfo.CurrentUICulture = new CultureInfo(currentLanguage);
        }

        protected override void OnStart()
        {
            Console.WriteLine("****OnStart");
        }

        protected override void OnSleep()
        {
            Console.WriteLine("****OnSleep");
            AppController.Instance.SaveData();
        }

        protected override void OnResume()
        {
            Console.WriteLine("****OnResume");
        }
    }
}
