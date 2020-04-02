using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
   public class TransactionsServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<Transactions> GetAllTransactions()
        {
            return context.Transactions.ToList();
        }

        public IEnumerable<Transactions> GetAllTransactionsByID(int budgetsID)
        {
            return context.Transactions.Where(x => x.BudgetsID == budgetsID).ToList();
        }

        public IEnumerable<Transactions> SearchTransactions(string searchTerm, int? budgetsID, int page, string orderByPrice, int recordSize, int? budgetTypeID, DateTime? searchTermStart = null, DateTime? searchTermEnd = null)
        {
            var transactions = context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
              transactions = transactions.Where(a => a.TransactionName.ToLower().Contains(searchTerm.ToLower()));
            }
            else
            {
                if (budgetsID != 0 && budgetsID != null)
                {
                    transactions = transactions.Where(a => a.BudgetsID == budgetsID.Value);
                }
                else if (budgetTypeID != 0 && budgetTypeID != null)
                {
                    transactions = transactions.Where(a => a.Budgets.KBudgetID == budgetTypeID.Value);
                }
                else if (searchTermStart != null && searchTermEnd != null)
                {
                    transactions = transactions.Where(a => a.Date >= searchTermStart && a.Date <= searchTermEnd);
                }
                else if (orderByPrice != "" || orderByPrice != null)
                {
                    if (orderByPrice == "Malejąco")
                    {
                        return transactions.OrderByDescending(x => x.Price).Take(recordSize).ToList();
                    }
                    else if (orderByPrice == "Rosnąco")
                    {
                        return transactions.OrderBy(x => x.Price).Take(recordSize).ToList();
                    }
                }
            }
               

                var skip = (page - 1) * recordSize;

            return transactions.OrderByDescending(x => x.Date).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchTransactionsCount(string searchTerm, int? budgetsID, int? budgetTypeID, DateTime? searchTermStart = null, DateTime? searchTermEnd = null)
        {
            var transactions = context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                transactions = transactions.Where(a => a.TransactionName.ToLower().Contains(searchTerm.ToLower()));
            }
            else
            {
                if (budgetsID != 0 && budgetsID != null)
                {
                    transactions = transactions.Where(a => a.BudgetsID == budgetsID.Value);
                }
                else if (budgetTypeID != 0 && budgetTypeID != null)
                {
                    transactions = transactions.Where(a => a.Budgets.KBudgetID == budgetTypeID.Value);
                }
                else if (searchTermStart != null && searchTermEnd != null)
                {
                    transactions = transactions.Where(a => a.Date >= searchTermStart && a.Date <= searchTermEnd);
                }
            }

            return transactions.Count();
        }

        public Transactions GetTransactionsByID(int ID)
        {
            return context.Transactions.Find(ID);
        }


        public bool SaveTransactions(Transactions transactions)
        {
            context.Transactions.Add(transactions);

            return context.SaveChanges() > 0;
        }

        public bool UpdateTransactions(Transactions transactions)
        {
            context.Entry(transactions).State = System.Data.Entity.EntityState.Modified;

            var exitingAccomodationPackage = context.Transactions.Find(transactions.ID);

            context.Entry(exitingAccomodationPackage).CurrentValues.SetValues(transactions);

            return context.SaveChanges() > 0;
        }

        public bool DeleteTransactions(Transactions transactions)
        {
            context.Entry(transactions).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
