using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Entities
{
    public class Budget
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int KBudgetID { get; set; }
        public virtual KBudget KBudgets { get; set; }
    }
}
