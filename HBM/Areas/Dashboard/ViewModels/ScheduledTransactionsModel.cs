using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class ScheduledTransactionsListingModel
    {
        public IEnumerable<ScheduledTransaction> ScheduledTransactions { get; set; }
        public string SearchTerm { get; set; }
    }

    public class ScheduledTransactionsActionModel
    {
        public int ID { get; set; }
        public string TransactionName { get; set; }
        public string Recipient { get; set; }
        public double Price { get; set; }
        public DateTime Data { get; set; }
    }
}