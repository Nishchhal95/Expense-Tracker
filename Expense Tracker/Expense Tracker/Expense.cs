using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker
{
    public class Expense
    {
        public int expenseId;
        public string expenseName;
        public float expenseAmount;
        public ExpenseType expenseType;

        public Expense(int expenseId, string expenseName, float expenseAmount, ExpenseType expenseType)
        {
            this.expenseId = expenseId;
            this.expenseName = expenseName;
            this.expenseAmount = expenseAmount;
            this.expenseType = expenseType;
        }
    }

    public enum ExpenseType
    {
        None,
        Food,
        Travel,
        Service
    }
}
