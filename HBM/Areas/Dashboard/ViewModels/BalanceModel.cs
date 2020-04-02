using HBM.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class BalanceListingModel
    {
        public DateTime? SearchTermStart { get; set; }
        public DateTime? SearchTermEnd { get; set; }
        public IEnumerable<Balance> Balances { get; set; }

        public float Incomes { get; set; }
        public float Expenses { get; set; }
        public float DebtCardRemaning { get; set; }
    }
}