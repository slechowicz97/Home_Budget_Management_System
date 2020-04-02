using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Entities
{
    public class Debts
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DebtPerson { get; set; }
        public string Executor { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public int KindOfDebtsID { get; set; }
        public virtual KindOfDebts KindsOfDebts { get; set; }
    }
}
