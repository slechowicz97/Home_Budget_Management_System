using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class SavingsListingModel
    {
        public IEnumerable<Savings> Savings { get; set; }
  
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
        public float AllSavings { get; set; }
    }

    public class SavingsActionModel
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime DateEnd { get; set; }
        public string Currency { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
    }
}