using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Model
{
    public class Expense
    {
        public static int _id = -1;
        public int id { get; set; }
        public float amount { get; set; }
        public ExpenseType expenseType { get; set; }
        public string description { get; set; }

        public Expense(float amount, ExpenseType expenseType, string description)
        {
            _id++;
            id = _id;
            this.amount = amount;
            this.expenseType = expenseType;
            this.description = description;
        }
    }

    public enum ExpenseType
    {
        Food,
        Service,
        Travel
    }
}
