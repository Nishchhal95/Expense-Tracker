using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_Tracker.Pages.Flyout
{
    public class ET_FlyoutFlyoutMenuItem
    {
        public ET_FlyoutFlyoutMenuItem()
        {
            TargetType = typeof(ET_FlyoutFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}