using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Entities
{
    public class Transactions
    {
        public int ID { get; set; }
        public string TransactionName { get; set; }
        public float Price { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }

        public int BudgetsID { get; set; }
        public string BudgetsName { get; set; }
        public virtual Budget Budgets { get; set; }
    }
}
