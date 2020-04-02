using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class DebtCardsListingModel
    {
        public IEnumerable<DebtCard> DebtCards { get; set; }
        public IEnumerable<Transactions> Transactions { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public DateTime PlannedTimeRefund { get; set; }

    }

    public class DebtCardsActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Limit { get; set; }
        public float Price { get; set; }
        public DateTime PlannedTimeRefund { get; set; }
        public IEnumerable<Transactions> Transactions { get; set; }

    }
}