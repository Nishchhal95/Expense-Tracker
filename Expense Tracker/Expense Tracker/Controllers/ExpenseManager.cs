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

        private static Dictionary<string, Func<Expense, IComparable>> nameToPropertyDitionary = new Dictionary<string, Func<Expense, IComparable>>()
        {
            {"Amount", x => x.amount } ,
            {"Description", x => x.description } ,
            {"Date", x => x.expenseDate }
        };

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

        public static List<Expense> GetExpensesByExpenseType(List<ExpenseType> expenseTypes)
        {
            return expenses.Where((x) => 
            {
                for (int i = 0; i < expenseTypes.Count; i++)
                {
                    if(x.expenseType == expenseTypes[i])
                    {
                        return true;
                    }
                }
                return false;
            }).ToList();
        }

        public static List<Expense> GetExpensesOnCustomFilter<T>(string searchText, string sortBy, List<ExpenseType> expenseTypes)
        {
            return expenses.Where((x) =>
            {
                for (int i = 0; i < expenseTypes.Count; i++)
                {
                    if (string.IsNullOrEmpty(searchText))
                    {
                        if (x.expenseType == expenseTypes[i])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (x.expenseType == expenseTypes[i] && x.description.Contains(searchText))
                        {
                            return true;
                        }
                    }
                    
                }
                return false;
            }).OrderBy(nameToPropertyDitionary[sortBy]).ToList();
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
