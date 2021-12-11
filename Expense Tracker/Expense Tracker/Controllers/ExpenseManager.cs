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

        private static ObservableCollection<ExpenseLimit> expensesLimits = new ObservableCollection<ExpenseLimit>();
        public static ReadOnlyObservableCollection<ExpenseLimit> ExpensesLimits { get { return new ReadOnlyObservableCollection<ExpenseLimit>(expensesLimits); } }


        private static List<string> dummyDescriptions = new List<string>()
        {
            "Electricity Bill",
            "Trip to Paris",
            "Water Bill",
            "Water Bottle",
            "Pasta",
            "Pizza",
            "Coffee",
            "University Fee",
            "Internet",
            "Netflix",
            "Amazon Prime",
            "Radio Bill",
            "Internet Bill",
            "Switzerland Trip"
        };

        public static void AddExpense(Expense expense)
        {
            expenses.Add(expense);
            StorageController.Instance.SetExpenses(expenses.ToList());
        }

        public static void RemoveExpense(Expense expense)
        {
            expenses.Remove(expense);
            StorageController.Instance.SetExpenses(expenses.ToList());
        }

        public static void RemoveExpense(int expenseId)
        {
            expenses.Remove(expenses.First(x => x.id == expenseId));
        }

        public static void RemoveAll()
        {
            expenses = new ObservableCollection<Expense>();
            StorageController.Instance.SetExpenses(expenses.ToList());
        }

        public static Expense GetExpense(int expenseId)
        {
            return expenses.First(x => x.id == expenseId);
        }

        public static void AddExpenses(List<Expense> expenses)
        {
            ExpenseManager.expenses = new ObservableCollection<Expense>(expenses);
            StorageController.Instance.SetExpenses(expenses.ToList());
        }

        public static void AddExpenses(ObservableCollection<Expense> expenses)
        {
            ExpenseManager.expenses = new ObservableCollection<Expense>(expenses);
            StorageController.Instance.SetExpenses(expenses.ToList());
        }




        public static void AddExpenseLimit(ExpenseLimit expenseLimit)
        {
            expensesLimits.Add(expenseLimit);
        }

        public static void AddExpenseLimitList(List<ExpenseLimit> expenseLimits)
        {
            expensesLimits = new ObservableCollection<ExpenseLimit>(expenseLimits);
        }

        //Create and Fill Dummy Data
        public static void CreateDummyData()
        {
            //TODO : Just to make sure we dont double up the data 
            Console.WriteLine("****Clearing Expenses Collection");
            expenses = new ObservableCollection<Expense>();

            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();
                Expense expense = new Expense(GetRandomNumberWithinRange(30, 5000), 
                    (ExpenseType)GetRandomNumberWithinRange(0, Enum.GetValues(typeof(ExpenseType)).Length), 
                    dummyDescriptions[random.Next(0, dummyDescriptions.Count)], 
                    RandomDay());
                expenses.Add(expense);
            }
        }

        private static int GetRandomNumberWithinRange(int starting, int end)
        {
            Random r = new Random();
            return r.Next(starting, end);
        }

        private static DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
