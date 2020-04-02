using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class BudgetListingModel
    {
        public IEnumerable<Budget> Budgets { get; set; }
        public int? KBudgetID { get; set; }
        public IEnumerable<KBudget> KBudgets { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class BudgetActionModel
    {
        public int ID { get; set; }

        public int KBudgetID { get; set; }
        public KBudget KBudget { get; set; }

        public string Name { get; set; }

        public IEnumerable<KBudget> KBudgets { get; set; }
    }
}