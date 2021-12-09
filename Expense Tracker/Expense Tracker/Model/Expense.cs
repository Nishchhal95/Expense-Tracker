using Expense_Tracker.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Model
{
    public class Expense
    {
        public static Dictionary<ExpenseType, string> expenseTypeToImageNameDictionary = new Dictionary<ExpenseType, string>()
        {
            {ExpenseType.Food, "food.png" },
            {ExpenseType.Service, "service.png" },
            {ExpenseType.Travel, "travel.png" },
        };
        public static int _id = -1;
        public int id { get; set; }
        public float amount { get; set; }
        public ExpenseType expenseType { get; set; }
        public string description { get; set; }

        public DateTime expenseDate { get; set; }
        public string Image { get; private set; }
        public string CurrencySign { get; private set; }

        public Expense(float amount, ExpenseType expenseType, string description, DateTime expenseDate)
        {
            _id++;
            id = _id;
            this.amount = amount;
            this.expenseType = expenseType;
            this.description = description;
            this.expenseDate = expenseDate;

            Image = expenseTypeToImageNameDictionary[this.expenseType];
            CurrencySign = StorageController.Instance.GetAppCurrency().CurrencySign;
        }
    }

    public enum ExpenseType
    {
        Food,
        Service,
        Travel
    }
}
