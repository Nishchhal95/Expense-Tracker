﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker.Pages.Flyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ET_FlyoutFlyout : ContentPage
    {
        public ListView ListView;

        public ET_FlyoutFlyout()
        {
            InitializeComponent();

            BindingContext = new ET_FlyoutFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class ET_FlyoutFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ET_FlyoutFlyoutMenuItem> MenuItems { get; set; }

            public ET_FlyoutFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<ET_FlyoutFlyoutMenuItem>(new[]
                {
                    new ET_FlyoutFlyoutMenuItem { Id = 0, Title = "Expenses" , TargetType = typeof(P_HomePage)},
                    new ET_FlyoutFlyoutMenuItem { Id = 1, Title = "Reminders", TargetType = typeof(P_RemindersPage)},
                    new ET_FlyoutFlyoutMenuItem { Id = 2, Title = "Configure", TargetType = typeof(P_ExpenseDetailsPage)},
                    new ET_FlyoutFlyoutMenuItem { Id = 3, Title = "Visualize Expenses", TargetType = typeof(P_VisualizeExpensesTabbedPage)},
                    new ET_FlyoutFlyoutMenuItem { Id = 4, Title = "Settings" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}