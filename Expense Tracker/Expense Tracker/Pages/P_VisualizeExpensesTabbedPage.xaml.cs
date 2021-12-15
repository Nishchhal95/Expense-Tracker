using Expense_Tracker.Controllers;
using Expense_Tracker.Model;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

namespace Expense_Tracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class P_VisualizeExpensesTabbedPage : TabbedPage
    {
        List<Entry> entries = new List<Entry>();
        private Dictionary<ExpenseType, SKColor> expenseTypeToColorDictionary = new Dictionary<ExpenseType, SKColor>()
        {
            {ExpenseType.Food,  SKColor.Parse("#52D726")},
            {ExpenseType.Service,  SKColor.Parse("#FFEC00")},
            {ExpenseType.Travel,  SKColor.Parse("#FF7300")},
            {ExpenseType.Others,  SKColor.Parse("#F305FF")}
        };

        public P_VisualizeExpensesTabbedPage()
        {
            InitializeComponent();
            List<Expense> expenseList = ExpenseManager.Expenses.ToList();

            for (int i = 0; i < Enum.GetNames(typeof(ExpenseType)).Length; i++)
            {
                ExpenseType expenseType = (ExpenseType)i;
                float total = expenseList.Where(x => x.expenseType == expenseType).Sum(x => x.amount);
                Entry entry = new Entry(total)
                {
                    Color = expenseTypeToColorDictionary[expenseType],
                    Label = expenseType.ToString(),
                    ValueLabel = StorageController.Instance.GetAppCurrency().CurrencySign + total.ToString(),
                    ValueLabelColor = expenseTypeToColorDictionary[expenseType],
                    TextColor = expenseTypeToColorDictionary[expenseType]
                };
                entries.Add(entry);
            }

            Chart1.Chart = new LineChart() 
            {
                Entries = entries, 
                LabelTextSize = 30f, 
                LabelOrientation = Orientation.Horizontal, 
                ValueLabelOrientation = Orientation.Horizontal,
                PointMode = PointMode.Square,
                PointSize = 10f
            };
            Chart2.Chart = new PieChart()
            {
                Entries = entries,
                LabelTextSize = 30f
            };
            Chart3.Chart = new BarChart() 
            {
                Entries = entries, LabelTextSize = 30f, 
                LabelOrientation = Orientation.Horizontal, 
                ValueLabelOrientation = Orientation.Horizontal
            };
        }

        private Entry ConvertExpenseToEntry(Expense expense)
        {
            Entry chartEntry = new Entry(expense.amount)
            {
                Color = SKColor.Parse("#FF1943"),
                Label = expense.expenseType.ToString(),
                ValueLabel = expense.amount.ToString()
            };

            return chartEntry;
        }
    }
}