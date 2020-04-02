using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Entities
{
    public class Savings
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime DateEnd { get; set; }
        public string Currency { get; set; }
    }
}
