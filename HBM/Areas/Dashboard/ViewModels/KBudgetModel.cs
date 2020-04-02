using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class KBudgetListingModel
    {
        public IEnumerable<KBudget> KBudgets { get; set; }
    }

    public class KBudgetActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}