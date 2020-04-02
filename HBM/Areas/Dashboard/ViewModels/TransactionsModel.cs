using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class TransactionListingModel
    {
        public IEnumerable<Transactions> Transactions { get; set; }
        public int? BudgetsID { get; set; }
        public string BudgetsName { get; set; }
        public IEnumerable<Budget> Budgets { get; set; }
        public IEnumerable<DebtCard> DebtCards { get; set; }
        public string SearchTerm { get; set; }

        public DateTime? SearchTermStart { get; set; }
        public DateTime? SearchTermEnd { get; set; }
        public float SumOfPrice { get; set; }
        public Pager Pager { get; set; }
    }

    public class TransactionActionModel
    {
        public int ID { get; set; }

        public int BudgetsID { get; set; }
        public string BudgetsName { get; set; }
        public virtual Budget Budget { get; set; }

        public string TransactionName { get; set; }
        public float Price { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public DateTime PlannedTimeRefund { get; set; }

        public IEnumerable<Budget> Budgets { get; set; }
        public IEnumerable<DebtCard> DebtCards { get; set; }
    }
}