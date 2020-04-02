using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class DebtsListingModel
    {
             public IEnumerable<Debts> Debts { get; set; }
            public int? KindOfDebtsID { get; set; }
            public IEnumerable<KindOfDebts> KindOfDebts { get; set; }
            public string SearchTerm { get; set; }
            public Pager Pager { get; set; }
        }

        public class DebtsActionModel
        {
       public int ID { get; set; }

        public int KindOfDebtsID { get; set; }
       public KindOfDebts KindOfDebt { get; set; }

        public string Name { get; set; }
        public string DebtPerson { get; set; }
        public string Executor { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<KindOfDebts> KindOfDebts { get; set; }
    }
}