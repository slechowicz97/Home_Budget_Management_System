using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class ScheduledTransactionsServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<ScheduledTransaction> GetAllScheduledTransactions()
        {
            return context.ScheduledTransactions.ToList();
        }

        public IEnumerable<ScheduledTransaction> SearchScheduledTransactions(string searchTerm)
        {
            var scheduledTransactions = context.ScheduledTransactions.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                scheduledTransactions = scheduledTransactions.Where(a => a.Recipient.ToLower().Contains(searchTerm.ToLower()));
            }

            return scheduledTransactions.OrderBy(x => x.Data).ToList();
        }

        public int SearchScheduledTransactionsCount(string searchTerm)
        {
            var scheduledTransactions = context.ScheduledTransactions.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                scheduledTransactions = scheduledTransactions.Where(a => a.Recipient.ToLower().Contains(searchTerm.ToLower()));
            }

            return scheduledTransactions.Count();
        }

        public ScheduledTransaction GetScheduledTransactionsByID(int ID)
        {
            return context.ScheduledTransactions.Find(ID);
        }


        public bool SaveScheduledTransaction(ScheduledTransaction scheduledTransaction)
        {
            context.ScheduledTransactions.Add(scheduledTransaction);

            return context.SaveChanges() > 0;
        }

        public bool UpdateScheduledTransaction(ScheduledTransaction scheduledTransaction)
        {
            context.Entry(scheduledTransaction).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteScheduledTransaction(ScheduledTransaction scheduledTransaction)
        {
            context.Entry(scheduledTransaction).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
