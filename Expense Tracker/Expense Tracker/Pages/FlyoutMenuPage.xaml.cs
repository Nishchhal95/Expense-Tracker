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
    public partial class FlyoutMenuPage : ContentPage
    {
        public ListView flyoutMenuListView;
        public FlyoutMenuPage()
        {
            InitializeComponent();
            flyoutMenuListView = FlyoutMenuPageListView;
        }
    }
}