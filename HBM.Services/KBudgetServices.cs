using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class KBudgetServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<KBudget> GetAllKBudgets()
        {        
            return context.KBudgets.ToList();
        }
        public KBudget GetKBudgetsByID(int ID)
        {
            return context.KBudgets.Find(ID);
        }

        public bool SaveKBudgets(KBudget kBudget)
        {
            context.KBudgets.Add(kBudget);

            return context.SaveChanges() > 0;
        }

        public bool UpdateKBudgets(KBudget kBudget)
        {
            context.Entry(kBudget).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteKBudgets(KBudget kBudget)
        {
            context.Entry(kBudget).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

    }
}
