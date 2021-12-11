using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Expense_Tracker.Model
{
    public class ExpenseLimit :INotifyPropertyChanged
    {
        public ExpenseType ExpenseType { get; set; }
        public float ExpenseMaxLimit { get; set; }
        private string currencySign;
        public string CurrencySign { get { return currencySign; } set { currencySign = value; OnPropertyChanged("CurrencySign"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
