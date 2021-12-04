using Expense_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Expense_Tracker.Controllers
{
    public static class ExpenseManager
    {
        private static ObservableCollection<Expense> expenses = new ObservableCollection<Expense>();
        public static ReadOnlyObservableCollection<Expense> Expenses { get { return new ReadOnlyObservableCollection<Expense>(expenses); } }

        public static void AddExpense(Expense expense)
        {
            expenses.Add(expense);
        }

        public static void RemoveExpense(Expense expense)
        {
            expenses.Remove(expense);
        }

        public static void RemoveExpense(int expenseId)
        {
            expenses.Remove(expenses.First(x => x.id == expenseId));
        }

        public static void RemoveAll()
        {
            expenses = new ObservableCollection<Expense>();
        }

        public static Expense GetExpense(int expenseId)
        {
            return expenses.First(x => x.id == expenseId);
        }

        public static void AddExpenses(List<Expense> expenses)
        {
            ExpenseManager.expenses = new ObservableCollection<Expense>(expenses);
        }

        public static void AddExpenses(ObservableCollection<Expense> expenses)
        {
            ExpenseManager.expenses = new ObservableCollection<Expense>(expenses);
        }

        //Create and Fill Dummy Data
        public static void CreateDummyData()
        {
            //TODO : Just to make sure we dont double up the data 
            Console.WriteLine("****Clearing Expenses Collection");
            expenses = new ObservableCollection<Expense>();

            for (int i = 0; i < 2; i++)
            {
                Expense expense = new Expense((100 * i) + 100, ExpenseType.Service, "Electricity Bill");
                expenses.Add(expense);
            }
        }
    }
}
