using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Entities
{
    public class ScheduledTransaction
    {
        public int ID { get; set; }
        public string TransactionName { get; set; }
        public string Recipient { get; set; }
        public double Price { get; set; }
        public DateTime Data { get; set; }
    }
}
