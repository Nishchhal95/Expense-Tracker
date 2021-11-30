using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Model
{
    public class Expense
    {
        public int id;
        public float amount;
        public ExpenseType expenseType;
        public string description;

        public Expense(int id, float amount, ExpenseType expenseType, string description)
        {
            this.id = id;
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
