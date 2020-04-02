using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class SavingsServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<Savings> GetAllSavings()
        {
            
            return context.Savings.ToList();
        }
        public IEnumerable<Savings> SearchSavings(string searchTerm, int page, int recordSize)
        {
            var savings = context.Savings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                savings = savings.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * recordSize;

            return savings.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchSavingsCount(string searchTerm)
        {
            var savings = context.Savings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                savings = savings.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
 
            return savings.Count();
        }

        public Savings GetSavingsByID(int ID)
        {
            return context.Savings.Find(ID);
        }


        public bool SaveSavings(Savings savings)
        {
            context.Savings.Add(savings);

            return context.SaveChanges() > 0;
        }

        public bool UpdateSavings(Savings savings)
        {
            context.Entry(savings).State = System.Data.Entity.EntityState.Modified;

            var exitingBudgets = context.Savings.Find(savings.ID);

            context.Entry(exitingBudgets).CurrentValues.SetValues(savings);

            return context.SaveChanges() > 0;
        }

        public bool DeleteSavings(Savings savings)
        {
            context.Entry(savings).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
