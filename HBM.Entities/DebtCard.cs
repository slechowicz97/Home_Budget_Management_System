using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Entities
{
    public class DebtCard
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Limit { get; set; }
        public float Price { get; set; }
        public DateTime PlannedTimeRefund { get; set; }
    }
}
